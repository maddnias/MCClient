using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTest.Protocol.Packets.Shared_Packets;

namespace MCTest.Protocol.Packets.Server_Packets
{
    class PLogin : MCPacket
    {
        public override byte PacketID { get { return 0x01; } }

        public int EntityID { get; set; }
        public string LevelType { get; set; }
        public byte GameMode { get; set; }
        public byte Dimension { get; set; }
        public byte Difficulty { get; set; }
        public byte Reserved { get; set; }
        public byte MaxPlayers { get; set; }

        public override int PacketSize
        {
            get { return LevelType.Length + 12 + 35; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
     
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            var mcs = new MinecraftStream(inputBuffer);

            mcs.ReadByte(); // ID

            EntityID = mcs.ReadInt32();
            LevelType = mcs.ReadString();
            GameMode = (byte) mcs.ReadByte();
            Dimension = (byte) mcs.ReadByte();
            Difficulty = (byte) mcs.ReadByte();
            Reserved = (byte) mcs.ReadByte();
            MaxPlayers = (byte) mcs.ReadByte();

        }
    }
}
