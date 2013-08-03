using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Structures.Compression.Base;
using uNet.Structures.Packets.Base;
using uNet.Structures.Settings.Base;

namespace MCTest.Protocol
{
    class MCSettings :OptionSet<MCProtocol>
    {
        public MCSettings(List<IPacket> packetTable, ICompressor packetCompressor, int receiveBufferSize = 1024)
            : base(packetTable, packetCompressor, receiveBufferSize)
        {
        }
    }
}
