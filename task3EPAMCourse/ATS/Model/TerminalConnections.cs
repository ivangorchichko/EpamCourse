using Task3EPAMCourse.ATS.Contracts;

namespace Task3EPAMCourse.ATS.Model
{
    public class TerminalConnections
    {
        public ITerminal Caller { get; set; }

        public ITerminal Answer { get; set; }
    }
}
