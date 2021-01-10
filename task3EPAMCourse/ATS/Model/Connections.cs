using System;
using task3EPAMCourse.ATS.Contracts;

namespace task3EPAMCourse.ATS.Model
{
    public class Connections : EventArgs
    {
        public ITerminal Caller { get; }
        public ITerminal Answer { get; }

        public Connections(ITerminal caller, ITerminal answer)
        {
            Caller = caller;
            Answer = answer;
        }
    }
}
