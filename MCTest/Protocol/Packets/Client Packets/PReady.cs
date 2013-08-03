using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Protocol.Packets.Client_Packets
{
    class PReady : MCPacket
    {
        public override byte PacketID { get { return 0xCD; } }

        public override int PacketSize
        {
            get { return 2; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteByte(PacketID);
            mcs.WriteByte(0x0);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            throw new NotImplementedException();
        }
    }
}
