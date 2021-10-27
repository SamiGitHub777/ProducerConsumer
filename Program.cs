using System;
using System.Threading.Tasks.Dataflow;

namespace ProducerConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var buffer = new BufferBlock<byte[]>(); //TPL Dataflow BufferBlock<T> type can be thought of as an unbounded buffer for data that enables
                                                    //synchronous and asynchronous producer/consumer scenarios
            Consumer consumer = new Consumer();
            Producer producer = new Producer();
            var consumerTask = consumer.ConsumeAsync(buffer);
            producer.Produce(buffer);
            int byteProcessed = consumerTask.Result;
            Console.WriteLine($"Byte processed : {byteProcessed}");
        }
    }
}
