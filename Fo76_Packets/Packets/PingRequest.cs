using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fo76_Packets.Packets
{
    public class PingRequest : Packet
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Body {
            public uint Timestamp;
            public ushort Bps;
            public ushort Unknown1;
            public byte Unknown2;
        }

        public Body PacketBody;
        public PingRequest(byte[] Data) : base(Data)
        {
            this.PacketBody = this.Reader.FromBinaryReader<Body>();
        }
    }
}
