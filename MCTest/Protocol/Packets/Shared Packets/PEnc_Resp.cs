using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTest.Cryptography;
using MCTest.Protocol.Packets.Client_Packets;

namespace MCTest.Protocol.Packets.Shared_Packets
{
    class PEnc_Resp : MCPacket
    {
        public override byte PacketID { get { return 0xFC; } }

        public byte[] SharedSecret { get; set; }
        public byte[] TokenResponse { get; set; }

        public override int PacketSize
        {
            get
            {
                if (SharedSecret == null || TokenResponse == null)
                    return 5;

                return SharedSecret.Length + TokenResponse.Length;
            }
        }

        public override void SerializePacket(Stream outputBuffer)
        {
            var mcs = new MinecraftStream(outputBuffer);

            mcs.WriteUInt8(PacketID);
            mcs.WriteInt16((short)SharedSecret.Length);
            mcs.WriteUInt8Array(SharedSecret);
            mcs.WriteInt16((short)TokenResponse.Length);
            mcs.WriteUInt8Array(TokenResponse);
        }

        public override void DeserializePacket(Stream inputBuffer)
        {
            Globals.Client.Socket.NetStream =
                        new MinecraftStream(new AesStream(Globals.Client.Socket.NetStream, Globals.SharedSecret));

            Globals.Client.Socket.SendPacket(new PReady());
        }
    }
}
