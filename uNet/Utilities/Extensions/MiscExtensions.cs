using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uNet.Server;

namespace uNet.Utilities.Extensions
{
    public static class MiscExtensions
    {
        public static string GenerateUID(this uNetServer server)
        {
            var uid = Guid.NewGuid().ToString();

            while (server.ConnectedPeers.FirstOrDefault(x => x.UID == uid) != null)
                uid = Guid.NewGuid().ToString();

            return uid;
        }
    }
}
