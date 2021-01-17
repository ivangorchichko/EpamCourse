using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.Billing.Contracts
{
    public interface ICaller
    {
        int CallerNumber { get; }

        ITerminal Terminal { get; }

        void ChangePortCondition(PortCondition condition);
    }
}
