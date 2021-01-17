using Task3EPAMCourse.ATS.Enums;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IPort
    {
        PortCondition Condition { get; }

        void ChangeCondition(PortCondition condition);
    }
}
