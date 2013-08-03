using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Structures.Packets.Base;

namespace MCTest.Protocol.Packets.Server_Packets
{
    class PEnc_Req : MCPacket
    {
        public override byte PacketID {
            get { return 0xFD; }
        }

        public string ServerID { get; set; }
        public byte[] Key { get; set; }
        public byte[] VToken { get; set; }

        public override int PacketSize
        {
            get { return ServerID.Length + Key.Length + VToken.Length + 8; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            throw new NotImplementedException();
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            var mcs = new MinecraftStream(inputBuffer);

            mcs.ReadByte();

            ServerID = mcs.ReadString();
            var keyLen = mcs.ReadInt16();
            Key = mcs.ReadUInt8Array(keyLen);
            var tokenLen = mcs.ReadInt16();
            VToken = mcs.ReadUInt8Array(tokenLen);

            Console.WriteLine("-> Received 0xFD encryption request from server...");

            Globals.Client.Auth.ProcessEncryptionRequest(this);
        }
    }
}
