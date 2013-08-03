using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Protocol.Packets.Shared_Packets
{
    class PKeepAlive : MCPacket
    {
        public override byte PacketID
        {
            get { return 0x0; }
        }

        public int Keep_AliveID { get; set; }

        public override int PacketSize
        {
            get { return 5; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteByte(PacketID);
            mcs.WriteInt32(Keep_AliveID);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            var mcs = new MinecraftStream(inputBuffer);

            mcs.ReadByte();
            Keep_AliveID = mcs.ReadInt32();

            Globals.Client.Socket.SendPacket(new PKeepAlive {Keep_AliveID = Keep_AliveID});
        }
    }
}
