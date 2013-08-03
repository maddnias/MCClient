using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Structures.Packets.Base;

namespace MCTest.Protocol.Packets
{
    public abstract class MCPacket : IPacket
    {
        public static Dictionary<byte, int> StaticPacketsizes = new Dictionary<byte, int>
                                                                            {
                                                                                { 0x04, 17 },
                                                                                { 0x07, 10 },
                                                                                { 0x08, 11 },
                                                                                { 0x0B, 34 },
                                                                                { 0x0C, 10 },
                                                                                { 0x0E, 12 },
                                                                                { 0x10, 3 },
                                                                                { 0x11, 15 },
                                                                                { 0x12, 6 },
                                                                                { 0x13, 10 },
                                                                                { 0x16, 9 },
                                                                                { 0x1A, 19 },
                                                                                { 0x1B, 11 },
                                                                                { 0x1C, 11 },
                                                                                { 0x1E, 5 },
                                                                                { 0x1F, 8 },
                                                                                { 0x20, 7 },
                                                                                { 0x21, 10 },
                                                                                { 0x22, 19 },
                                                                                { 0x23, 6 },
                                                                                { 0x26, 6 },
                                                                                { 0x27, 10 },
                                                                                { 0x29, 9 },
                                                                                { 0x2A, 6 },
                                                                                { 0x2B, 9 },
                                                                                { 0x35, 13 },
                                                                                { 0x36, 15 },
                                                                                { 0x37, 18 },
                                                                                { 0x3D, 19 },
                                                                                { 0x46, 3 },
                                                                                { 0x47, 18 },
                                                                                { 0x65, 2 },
                                                                                { 0x69, 6 },
                                                                                { 0x6A, 5 },
                                                                                { 0x6C, 3 },
                                                                                { 0x85, 14 },
                                                                                { 0xC8, 9 },
                                                                                { 0xCA, 10 },
                                                                                { 0xCD, 2 },
                                                                                { 0xFE, 2 }
                                                                            };

        public abstract byte PacketID { get; }

        //NOT USED
        public short ID {
            get { return -1; }
        }

        public abstract int PacketSize { get; }

        public abstract void SerializePacket(Stream outputBuffer);
        public abstract void DeserializePacket(Stream inputBuffer);
    }
}
