using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCTest.Protocol;
using MCTest.Protocol.Packets;
using MCTest.Protocol.Packets.Client_Packets;
using MCTest.Protocol.Packets.Shared_Packets;
using uNet.Client;
using uNet.Structures.Settings;

namespace MCTest
{
    public class MCClient
    {
        internal byte[] SharedSecret;
        public readonly uNetClient Socket;
        public Authenticator Auth;

        public MCClient(string host, int port)
        {
            Socket = new uNetClient(host, (uint)port, new MCSettings(null, null));
            Globals.Client = this;
            Socket.OnConnected += _client_OnConnected;
        }

        public void Login(string username, string password)
        {
            Auth = new Authenticator(username, password);
            Socket.Connect();
        }

        public void SendMessage(string message)
        {
            // Make sure authentication process is done before message is sent
            while (!Auth.Authenticated)
                Thread.Sleep(1);

            Socket.SendPacket(new PChat(message));
        }

        void _client_OnConnected(object sender, uNet.Structures.Events.ClientEventArgs e)
        {
            Auth.BeginAuthentication();
        }

        // If we don't continuously send a packet to the server telling it we're real
        // It'll disconnect us
        internal void KeepAlive()
        {
            var T = new Thread(() =>
            {
                while (true)
                {
                    Globals.Client.Socket.SendPacket(new PPlayer());
                    Thread.Sleep(50);
                }
            });

            T.Start();
        }

    }
}
