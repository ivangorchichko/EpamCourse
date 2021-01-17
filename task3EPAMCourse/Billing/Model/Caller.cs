using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.Billing.Model
{
    public class Caller : ICaller
    {
        public int CallerNumber { get; }

        public ITerminal Terminal { get; }

        public Contract Contract { get; } = new Contract();

        public Caller(int callerNumber, ITerminal terminal, IPort port)
        {
            CallerNumber = callerNumber;
            Terminal = terminal;
            Terminal.ChangePort(port);
        }

        public void ChangePortCondition(PortCondition condition)
        {
            Terminal.Port.ChangeCondition(condition);
            Console.WriteLine($"Port condition changed on {condition} by user {CallerNumber}");
        }
    }
}
