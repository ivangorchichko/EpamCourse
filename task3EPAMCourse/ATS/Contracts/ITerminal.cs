using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ITerminal
    {
        void ChangeTerminalCondition(TerminalCondition condition);

        void ChangePortSourse(IPort port);

        IPort Port { get; }

        TerminalCondition TerminalCondition { get; }

        void Calling(ICaller phone);

        void AceptCalling(ICaller caller);

        void StopCalling(ICaller secondCaller);
    }
}
