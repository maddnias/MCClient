using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using MCTest.Cryptography;
using MCTest.Protocol.Packets;
using MCTest.Protocol.Packets.Server_Packets;
using MCTest.Protocol.Packets.Shared_Packets;

namespace MCTest
{
    public class Authenticator
    {
        public bool Authenticated = false;
        public string Username;
        public string Password;

        private string _username;
        private string _sessionID;

        public Authenticator(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void BeginAuthentication()
        {
            Console.WriteLine("-> Attempting to login [{0}:{1}]...", Username, Password);

            if (CreateSession(Username, Password))
            {
                Globals.Client.Socket.SendPacket(new PHandshake());
            }
            else
                throw new Exception("Failed to create session!");
        }

        private bool CreateSession(string username, string password)
        {
            var resp =
                new WebClient().DownloadString(
                    string.Format("https://login.minecraft.net?user={0}&password={1}&version=13",
                                  username, password));
            string[] chunks;

            if ((chunks = resp.Split(':')).Length != 5)
                return false;

            Console.WriteLine("-> Online authentication successful...");

            _username = chunks[2];
            _sessionID = chunks[3];

            return true;
        }

        // Thanks https://github.com/SirCmpwn/Craft.Net
        public void ProcessEncryptionRequest(MCPacket requestPacket)
        {
            var packet = requestPacket as PEnc_Req;
            new Random(Guid.NewGuid().GetHashCode()).NextBytes(Globals.SharedSecret);

            if (packet.ServerID != "-" && _sessionID != null) // Online mode
            {
                var data = Encoding.ASCII.GetBytes(packet.ServerID)
                                   .Concat(Globals.SharedSecret)
                                   .Concat(packet.Key).ToArray();
                var hash = JavaHexDigest.Create(Encoding.ASCII.GetString(data));
                var webClient = new WebClient();
                string result = webClient.DownloadString("http://session.minecraft.net/game/joinserver.jsp?user=" +
                                                         Uri.EscapeUriString(_username) +
                                                         "&sessionId=" + Uri.EscapeUriString(_sessionID) +
                                                         "&serverId=" + Uri.EscapeUriString(hash));

                if (result != "OK")
                    throw new Exception();
            }

            Console.WriteLine("-> Session successfully created...");

            var parser = new AsnKeyParser(packet.Key);
            var key = parser.ParseRSAPublicKey();

            var rsaCrypto = new RSACryptoServiceProvider();
            rsaCrypto.ImportParameters(key);
            var encSharedSecret = rsaCrypto.Encrypt(Globals.SharedSecret, false);
            var encVerification = rsaCrypto.Encrypt(packet.VToken, false);

            Console.WriteLine("-> SharedSecret encrypted [{0}]", BitConverter.ToString(encSharedSecret));
            Console.WriteLine("-> Verification Token encrypted [{0}]", BitConverter.ToString(encVerification));

            Globals.Client.Socket.SendPacket(new PEnc_Resp
                                                 {
                                                     SharedSecret = encSharedSecret,
                                                     TokenResponse = encVerification
                                                 });

            Console.WriteLine("-> Responded to encryption request...");
        }
    }
}
