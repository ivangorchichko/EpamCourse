using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ICaller
    {
        int CallerNumber { get; }
        ITerminal Terminal { get; }
        void ChangePortCondition(PortCondition condition);
    }
}
