using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fo76_Packets.Packets
{
    public class Message : Packet
    {
        public struct Body {
            public byte SequenceNumber;
            public byte FragmentCount;
            public byte FragmentNumber;
            public uint Unknown1;
            public byte Unknown2;
            public uint Unknown3;
            public ushort CompressedMessageSize;
            public ushort Unknown8;
            public byte[] MessageData;
            public uint Unknown5;
            public byte Unknown6;
            public uint SnapshotSequence;
            public uint BaseSequence;
            public uint CompressedBodyMessageSize;
            public uint UncompressedBodyMessageSize;
            public ushort ComponentCount;
            public uint Unknown7;
            public byte[] FragmentMessageData;

            public int UncompressedMessageSize;
        }

        public Body PacketBody;
        public Message(byte[] Data) : base(Data)
        {
            PacketBody = new Body();

            this.PacketBody.SequenceNumber = this.Reader.ReadByte();
            this.PacketBody.FragmentCount = this.Reader.ReadByte();
            this.PacketBody.FragmentNumber = this.Reader.ReadByte();
            this.PacketBody.Unknown1 = this.Reader.ReadUInt32();
            this.PacketBody.Unknown2 = this.Reader.ReadByte();
            this.PacketBody.Unknown3 = this.Reader.ReadUInt32();
            this.PacketBody.CompressedMessageSize = (ushort)((this.Reader.ReadUInt16() & 0x7FFF) - 2);
            this.PacketBody.Unknown8 = this.Reader.ReadUInt16();
            this.PacketBody.MessageData = new byte[4096]; //no decompressed size is being sent with the packet here?
            
            byte[] compressedMessageData = new byte[this.PacketBody.CompressedMessageSize + 0x10];
            this.Reader.Read(compressedMessageData, 0, this.PacketBody.CompressedMessageSize);
            LightweightCompression lWCompression = new LightweightCompression();
            lWCompression.Start(compressedMessageData, this.PacketBody.CompressedMessageSize, false);
            this.PacketBody.UncompressedMessageSize = lWCompression.Read(this.PacketBody.MessageData, 4096);

            this.PacketBody.Unknown5 = this.Reader.ReadUInt32();
            
            var dwLoop = 0;
            do
            {
                this.PacketBody.Unknown6 |= (byte)(this.Reader.ReadByte() << dwLoop);
                dwLoop += 8;
            } while (dwLoop < 32);
            
            if(this.PacketBody.Unknown5 != 1) {
                this.PacketBody.SnapshotSequence = this.Reader.ReadUInt32();
                this.PacketBody.BaseSequence = this.Reader.ReadUInt32();
                this.PacketBody.CompressedBodyMessageSize = this.Reader.ReadUInt32() & 0x7FFFFFFF;
                this.PacketBody.UncompressedBodyMessageSize = this.Reader.ReadUInt32();
                this.PacketBody.ComponentCount = this.Reader.ReadUInt16();
                this.PacketBody.Unknown7 = this.Reader.ReadUInt32();
                this.PacketBody.FragmentMessageData = new byte[this.Reader.BaseStream.Length - this.Reader.BaseStream.Position];
                this.Reader.Read(this.PacketBody.FragmentMessageData, 0, this.PacketBody.FragmentMessageData.Length);
            }
        }
    }
}
