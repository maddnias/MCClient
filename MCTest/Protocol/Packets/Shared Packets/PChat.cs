using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Protocol.Packets.Shared_Packets
{
    class PChat : MCPacket
    {
        public PChat()
        {
            
        }

        public PChat(string message)
        {
            Message = message;
        }

        public override byte PacketID
        {
            get { return 0x03; }
        }

        public string Message { get; set; }

        public override int PacketSize
        {
            get { return 0; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteByte(PacketID);
            mcs.WriteString(Message);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
           
        }
    }
}
