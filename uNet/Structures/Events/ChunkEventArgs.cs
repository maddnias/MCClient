using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Server;

namespace uNet.Structures.Events
{
    public class ChunkEventArgs : PeerEventArgs
    {
        public byte[] Chunk { get; set; }

        public ChunkEventArgs(Peer peer, byte[] chunk)
            : base(peer)
        {
            Chunk = chunk;
        }
    }
}
