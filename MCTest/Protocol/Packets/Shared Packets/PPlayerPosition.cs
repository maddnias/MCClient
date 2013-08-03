using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Protocol.Packets.Shared_Packets
{
    class PPlayerPosition : MCPacket
    {
        public override byte PacketID { get { return 0x0D; } }

        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Stance { get; set; }
        public double PosZ { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public bool OnGround { get; set; }

        public override int PacketSize
        {
            get { return 42; }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteByte(PacketID);
            mcs.WriteDouble(PosX);
            mcs.WriteDouble(PosY);
            mcs.WriteDouble(Stance);
            mcs.WriteDouble(PosZ);
            mcs.WriteBoolean(OnGround);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            var mcs = new MinecraftStream(inputBuffer);

            mcs.ReadByte();
            PosX = mcs.ReadDouble();
            PosY = mcs.ReadDouble();
            Stance = mcs.ReadDouble();
            PosZ = mcs.ReadDouble();
            OnGround = mcs.ReadBoolean();

            if (!Globals.Client.Auth.Authenticated)
            {
                Globals.Client.Auth.Authenticated = true;
                Globals.Client.KeepAlive();

                Console.WriteLine("-> Received initial position [{0}|{1}|{2}]...", PosX, PosY, PosZ);
                Console.WriteLine("-> Client is authenticated!");
            }
        }
    }
}
