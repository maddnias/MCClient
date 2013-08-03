using uNet.Server;

namespace uNet.Structures.Events
{
    public delegate void ClientEventHandler(object sender, ClientEventArgs e);

    public delegate void PeerEventHandler(object sender, PeerEventArgs e);
    public delegate void PacketEventHandler(object sender, PacketEventArgs e);
    public delegate void ChunkEventHandler(object sender, ChunkEventArgs e);
}
