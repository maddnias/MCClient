using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Protocol.Packets.Server_Packets
{
    class PSpawnPosition : MCPacket
    {
        public override byte PacketID { get { return 0x06; } }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }

        public override int PacketSize
        {
            get { return 13; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            //throw new NotImplementedException();
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            var mcs = new MinecraftStream(inputBuffer);

            mcs.ReadByte();

            PosX = mcs.ReadInt32();
            PosY = mcs.ReadInt32();
            PosZ = mcs.ReadInt32();
        }
    }
}
