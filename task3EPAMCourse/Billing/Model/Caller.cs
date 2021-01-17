using System;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.Billing.Model
{
    public class Caller : ICaller
    {
        public Caller(int callerNumber, ITerminal terminal, IPort port)
        {
            CallerNumber = callerNumber;
            Terminal = terminal;
            Terminal.ChangePort(port);
        }

        public int CallerNumber { get; }

        public ITerminal Terminal { get; }

        public Contract Contract { get; } = new Contract();

        public PortCondition ChangePortCondition(PortCondition condition)
        {
            Terminal.Port.ChangeCondition(condition);
            return condition;
        }
    }
}
