using System.Threading.Tasks.Dataflow;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    public class Consumer
    {
        /*
         * async modifier to specify that a method, lambda expression, or anonymous method is asynchronous
         * An async method runs synchronously until it reaches its first await expression,
         * at which point the method is suspended until the awaited task is complete.
         * In the meantime, control returns to the caller of the method : do not block the caller's thread
         * Return types :
         *      Task : if no meaningful value is returned when the method is completed. That is, a call to the method returns a Task, but when the Task is completed, any await expression that's awaiting the Task evaluates to void
         *      Task<TResult> : if the return statement of the method specifies an operand of type TResult
         *      void : async void methods are generally discouraged for code other than event handlers because callers cannot await those methods and must implement a different mechanism to report successful completion or error conditions.
        */
        public async Task<int> ConsumeAsync(ISourceBlock<byte[]> table)
        {
            Console.WriteLine("Starting consumer...");
            int byteProcessed = 0;
            /*
             * If the method that the async keyword modifies doesn't contain an await expression or statement, the method executes synchronously
             * The await operator suspends evaluation of the enclosing async method until the asynchronous operation represented by its operand completes. When the asynchronous operation completes, the await operator returns the result of the operation, if any. 
             * When the await operator is applied to the operand that represents an already completed operation, it returns the result of the operation immediately without suspension of the enclosing method. 
             * The await operator doesn't block the thread that evaluates the async method. 
             * When the await operator suspends the enclosing async method, the control returns to the caller of the method.
             */
            while (await table.OutputAvailableAsync())
            {
                byte[] data = await table.ReceiveAsync();
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Consuming {data.Length} items");
                byteProcessed += data.Length;
            }
            return byteProcessed;
        }
    }   
}
