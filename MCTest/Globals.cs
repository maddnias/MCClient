using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Client;

namespace MCTest
{
    internal static class Globals
    {
        public static MCClient Client;
        public static byte[] SharedSecret = new byte[16];
    }
}
