using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;

namespace Task3EPAMCourse.Billing.Contracts
{
    public interface ICaller
    {
        int CallerNumber { get; }

        ITerminal Terminal { get; }

        PortCondition ChangePortCondition(PortCondition condition);
    }
}
