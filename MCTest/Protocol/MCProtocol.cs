using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTest.Protocol.Packets;
using MCTest.Protocol.Packets.Server_Packets;
using MCTest.Protocol.Packets.Shared_Packets;
using uNet.Structures.Packets.Base;
using uNet.Utilities;

namespace MCTest.Protocol
{
    class MCProtocol : PacketProcessor
    {
        bool _flag;

        private readonly List<MCPacket> ServerPackets = new List<MCPacket>
                                                  {
                                                      new PHandshake(),
                                                      new PEnc_Req(),
                                                      new PEnc_Resp(),
                                                      new PLogin(),
                                                      new PPlayerPosition(),
                                                  };

        public override async Task SendPacket(IPacket packet, Stream netStream)
        {
            var ms = new MemoryStream();

            packet.SerializePacket(ms);

            await netStream.WriteAsync(ms.ToArray(), 0, (int)ms.Length);
        }

        public override IPacket ParsePacket(byte[] rawData)
        {
            try
            {
                var packet = ServerPackets.FirstOrDefault(x => x.PacketID == rawData[0]);

                using (var ms = new MemoryStream(rawData))
                    packet.DeserializePacket(ms);

                return packet;
            }
            catch
            {
                return new PUnknown();
            }
        }

        public override List<IPacket> ProcessData(List<byte> buffer)
        {
            var packet = ParsePacket(buffer.ToArray());
            buffer.RemoveRange(0, packet.PacketSize);

            // Clear buffer from packets we're not able to sync with anyway
            if (buffer.Count >= 10000)
                buffer.RemoveRange(1024, 10000 - 1024);

            // There's junk data inbetween the PlayerPosition packet and the last packet
            // So we have to skip ahead until we hit 0x0D
            if (packet is PUnknown && _flag == false)
            {
                _flag = true;
                var a = buffer.FindIndex(x => x == 0x0D);
                buffer.RemoveRange(0, a);
                buffer.RemoveRange(42, buffer.Count - 42);

                var _packet = ParsePacket(buffer.ToArray());
                return new List<IPacket> { _packet };
            }

            
            return new List<IPacket> {packet};
        }
    }
}
