using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Structures.Packets.Base;

namespace MCTest.Protocol.Packets
{
    class PHandshake : MCPacket
    {
        public override byte PacketID{
            get { return 0x2; }
        }

        public override int PacketSize
        {
            get { return 0; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteUInt8(0x2);
            mcs.WriteUInt8(0x4a);
            mcs.WriteString(Globals.Client.Auth.Username);
            mcs.WriteString("127.0.0.1");
            mcs.WriteInt32(25565);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            throw new NotImplementedException();
        }
    }
}
