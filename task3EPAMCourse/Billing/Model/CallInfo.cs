using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Contracts;

namespace task3EPAMCourse.Billing.Model
{
    public class CallInfo
    {
        public ITerminal From { get; set; }
        public ITerminal To { get; set; }
        public DateTime DateTimeStart { get; set; }
        public TimeSpan Duration { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return $"From: {From.Number}\t" +
                $"To: {To.Number}\n" +
                $"Started at: {DateTimeStart:F}\t" +
                $"Duration: {Duration:hh\\:mm\\:ss}\n" +
             //   $"State: {CallState}\t" +
                $"Cost: {Cost:F2}";
        }

    }
}
