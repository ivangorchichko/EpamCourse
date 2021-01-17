using System;
using task3EPAMCourse.ATS.Contracts;

namespace task3EPAMCourse.ATS.Model
{
    public class TerminalConnectionsEventArgs : EventArgs
    {
        public ITerminal Caller { get; set; }
        public ITerminal Answer { get; set; }
    }
}
