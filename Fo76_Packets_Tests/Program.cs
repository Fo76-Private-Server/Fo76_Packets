using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fo76_Packets;
using Fo76_Packets.Packets;

namespace Fo76_Packets_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Packet packet = Packet.ParsePacket(TestData.MessageTest1);
            Console.WriteLine(packet.Head.Session.ToString("X"));
            Console.WriteLine(packet.Head.PacketType);
            Message message = (Message)packet;
            Console.WriteLine("SequenceNumber: " + message.PacketBody.SequenceNumber.ToString("X"));
            Console.WriteLine("FragmentCount: " + message.PacketBody.FragmentCount.ToString("X"));
            Console.WriteLine("FragmentNumber: " + message.PacketBody.FragmentNumber.ToString("X"));
            Console.WriteLine("Unknown1: " + message.PacketBody.Unknown1.ToString("X"));
            Console.WriteLine("Unknown2: " + message.PacketBody.Unknown2.ToString("X"));
            Console.WriteLine("Unknown3: " + message.PacketBody.Unknown3.ToString("X"));
            Console.WriteLine("CompressedMessageSize: " + message.PacketBody.CompressedMessageSize.ToString("X"));
            Console.WriteLine("UncompressedMessageSize: " + message.PacketBody.Unknown8.ToString("X"));
            Console.WriteLine("MessageData: " + message.PacketBody.MessageData.ToHexString());
            Console.WriteLine("Unknown5: " + message.PacketBody.Unknown5.ToString("X"));
            Console.WriteLine("Unknown6: " + message.PacketBody.Unknown6.ToString("X"));
            Console.WriteLine("SnapshotSequence: " + message.PacketBody.SnapshotSequence.ToString("X"));
            Console.WriteLine("BaseSequence: " + message.PacketBody.BaseSequence.ToString("X"));
            Console.WriteLine("CompressedBodyMessageSize: " + message.PacketBody.CompressedBodyMessageSize.ToString("X"));
            Console.WriteLine("UncompressedBodyMessageSize: " + message.PacketBody.UncompressedBodyMessageSize.ToString("X"));
            Console.WriteLine("ComponentCount: " + message.PacketBody.ComponentCount.ToString("X"));
            Console.WriteLine("Unknown7: " + message.PacketBody.Unknown7.ToString("X"));
            Console.WriteLine("FragmentMessageData: " + message.PacketBody.FragmentMessageData.ToHexString());

            Console.WriteLine("End");
            Console.ReadLine();
        }

        static void TestPingReply()
        {
            Packet packet = Packet.ParsePacket(TestData.PingReplyTest1);
            Console.WriteLine("Session: " + packet.Head.Session.ToString("X"));
            Console.WriteLine("PacketType: " + packet.Head.PacketType);
            PingReply pingReply = (PingReply)packet;
            Console.WriteLine("Timestamp: " + pingReply.PacketBody.Timestamp.ToString("X"));
            Console.WriteLine("Bps: " + pingReply.PacketBody.Bps.ToString("X"));
            Console.WriteLine("Unknown1: " + pingReply.PacketBody.Unknown1.ToString("X"));
            Console.WriteLine("Unknown2: " + pingReply.PacketBody.Unknown2.ToString("X"));
        }

        static void TestPingRequest() {
            Packet packet = Packet.ParsePacket(TestData.PingRequestTest1);
            Console.WriteLine("Session: " + packet.Head.Session.ToString("X"));
            Console.WriteLine("PacketType: " + packet.Head.PacketType);
            PingRequest pingRequest = (PingRequest)packet;
            Console.WriteLine("Timestamp: " + pingRequest.PacketBody.Timestamp.ToString("X"));
            Console.WriteLine("Bps: " + pingRequest.PacketBody.Bps.ToString("X"));
            Console.WriteLine("Unknown1: " + pingRequest.PacketBody.Unknown1.ToString("X"));
            Console.WriteLine("Unknown2: " + pingRequest.PacketBody.Unknown2.ToString("X"));
        }
    }
}
