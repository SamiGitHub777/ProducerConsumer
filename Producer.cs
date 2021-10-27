using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace ProducerConsumer
{
    public class Producer
    {
        public void Produce(ITargetBlock<byte[]> table)
        {
            Console.WriteLine("Starting producer...");
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                byte[] buffer = new byte[1024];
                rnd.NextBytes(buffer);
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Producing {buffer.Length} items");
                table.Post(buffer);
            }
            table.Complete();
        }
    }
}
