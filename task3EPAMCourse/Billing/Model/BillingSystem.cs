using System.Timers;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.Billing.Model
{
    public class BillingSystem : IBilling
    {
        public static double time { get; private set; } = 0.0;
        public Timer Timer { get; } = new Timer(10);
        public BillingSystem()
        {
           
        }

        public void StartConnecting(ICaller caller1, ICaller caller2)
        {
            Timer.Elapsed += OnCall;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        public void StopConnecting(ICaller caller1, ICaller caller2)
        {
            Timer.Stop();
        }

        public static void OnCall(object sender, ElapsedEventArgs e)
        {
            time += 0.01;
        }
    }
}
