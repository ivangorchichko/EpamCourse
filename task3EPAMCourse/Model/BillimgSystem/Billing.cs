using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using task3EPAMCourse.Contracts;

namespace task3EPAMCourse.Model.BillimgSystem
{
    public class Billing
    {
        public static double time { get; private set; } = 0.0;
        public Timer Timer { get; } = new Timer(10);
        public Billing()
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
