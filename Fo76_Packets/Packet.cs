using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Fo76_Packets.Packets;

namespace Fo76_Packets
{
    public abstract class Packet
    {
        //should bittest instead to differentiate
        public enum MessageType : byte
        {
            Token = 0,
            PingRequest = 0x80,
            PingReply = 0x81,
            Message = 0x82
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct PacketHead {
            public ushort Session;
            public byte PacketType;
        }

        public PacketHead Head;
        public BinaryReader Reader;

        public Packet(byte[] Data) {
            this.Reader = new BinaryReader(new MemoryStream(Data));
            this.Head = this.Reader.FromBinaryReader<PacketHead>();
        }

        public static Packet ParsePacket(byte[] Data) {
            switch((MessageType)Data[2]) {
                case MessageType.Message:
                    return new Message(Data);
                case MessageType.PingReply:
                    return new PingReply(Data);
                case MessageType.PingRequest:
                    return new PingRequest(Data);
                case MessageType.Token:
                    return new Token(Data);
                default:
                    throw (new Exception("Unknown Packet"));
            }
        }
    }
}
