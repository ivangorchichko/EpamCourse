using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task3EPAMCourse.Contracts;
using task3EPAMCourse.Enums;
using task3EPAMCourse.Model.ATS;

namespace task3EPAMCourse.Model.BillimgSystem
{
    public class Caller : ICaller
    {
        public int CallerNumber { get; }
        public ITerminal Terminal { get; }
        public Contract Contract { get; } = new Contract();

        public Caller() { }
        public Caller(int callerNumber, ITerminal terminal)
        {
            CallerNumber = callerNumber;
            Terminal = terminal;
        }

        public void ChangePortCondition(PortCondition condition)
        {
            Terminal.Port.ChangeCondition(condition);
        }
    }
}
