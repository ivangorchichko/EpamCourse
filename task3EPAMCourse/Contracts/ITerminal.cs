using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Enums;

namespace task3EPAMCourse.Contracts
{
    public interface ITerminal
    {
        void ChangeTerminalCondition(TerminalCondition condition);

        IPort Port { get; }
        void ConnectedOnPort(IPort port);
    }
}
