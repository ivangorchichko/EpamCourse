using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IPort
    {
        PortCondition Condition { get; }

        void ChangeCondition(PortCondition condition);
    }
}
