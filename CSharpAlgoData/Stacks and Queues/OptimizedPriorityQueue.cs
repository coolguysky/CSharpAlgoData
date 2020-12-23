using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Stacks_and_Queues
{
    class OptimizedPriorityQueue
    {
    }

    public class IncomingCallPriority
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime CallTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Consultant { get; set; }
        public bool IsPriority { get; set; }
    }

    public class CallCenter
    {
        private int _counter = 0;
        public SimplePriorityQueue<IncomingCallPriority> PriorityCalls { get; private set; }

        public CallCenter()
        {
            PriorityCalls = new SimplePriorityQueue<IncomingCallPriority>();
        }

        public void Call(int clientId, bool isPriority = false)
        {
            IncomingCallPriority call = new IncomingCallPriority()
            {
                Id = ++_counter,
                ClientId = clientId,
                CallTime = DateTime.Now,
                IsPriority = isPriority
            };
            PriorityCalls.Enqueue(call, isPriority ? 0 : 1); //where the priority occurs
        }

        public IncomingCallPriority Answer(string consultant)
        {
            if (PriorityCalls.Count > 0)
            {
                IncomingCallPriority call = PriorityCalls.Dequeue();
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
            return PriorityCalls.Count > 0;
        }
    }

}
