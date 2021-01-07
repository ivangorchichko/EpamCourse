using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Contracts;
using task3EPAMCourse.Enums;

namespace task3EPAMCourse.Model.ATS
{
    public class Terminal : ITerminal
    {
        public string Number { get; }
        public IPort Port { get; private set; }
        public TerminalCondition TerminalCondition { get; private set; }

        public Terminal(string number, TerminalCondition condition)
        {
            Number = number;
            TerminalCondition = condition;
        }
        public void ConnectedOnPort(IPort port)
        {
            Port = port;
        }

        public void ChangeTerminalCondition(TerminalCondition condition)
        {
            TerminalCondition = condition;
        }
    }
}
