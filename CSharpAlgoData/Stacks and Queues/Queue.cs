using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpAlgoData.Stacks_and_Queues
{
    class Queue{}
    class QueueCallCenter
    {
        private int _counter = 0;
        public Queue<IncomingCall> Calls { get; private set; }
        
        public QueueCallCenter()
        {
            Calls = new Queue<IncomingCall>();
        }

        public void Call(int clinetId)
        {
            IncomingCall call = new IncomingCall()
            {
                Id = ++_counter,
                ClientId = clinetId,
                CallTime = DateTime.Now
            };
            Calls.Enqueue(call); // pretty simple stuff
        }

        public IncomingCall Answer(string consultant)
        {
            if(Calls.Count > 0)
            {
                IncomingCall call = Calls.Dequeue();
                call.Consultant = consultant;
                call.StartTime = DateTime.Now;
                return call;
            }
            return null;
        }

        public void End(IncomingCall call)
        {
            call.EndTime = DateTime.Now;
        }

        public bool AreWaitingCalls()
        {
            return Calls.Count > 0;
        }
    }
    public class IncomingCall
    {
        public int Id { get; set; } 
        public int ClientId { get; set; }
        public DateTime CallTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Consultant { get; set; }
    }

    //static void CallCenter()
    //{
    //    Random random = new Random();

    //    QueueCallCenter center = new QueueCallCenter();
    //    center.Call(1234);
    //    center.Call(5678);
    //    center.Call(1468);
    //    center.Call(9641);

    //    while (center.AreWaitingCalls())
    //    {
    //        IncomingCall call = center.Answer("Marcin");
    //        Log($"Call #{call.Id} from {call.ClientId} is answered by { call.Consultant}."); 
    //        Thread.Sleep(random.Next(1000, 10000));
    //        center.End(call);
    //        Log($"Call #{call.Id} from {call.ClientId} is ended by { call.Consultant}."); 
    //    }
    //}
    //static void Log(string text)
    //{
    //    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]{text}");
    //}
}
