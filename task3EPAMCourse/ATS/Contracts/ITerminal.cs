using System;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ITerminal
    {
        event EventHandler<Connections> Call;
        event EventHandler<Connections> AceptCall;
        event EventHandler<Connections> StopCall;
        event EventHandler<Connections> DropCall;
        void ChangeTerminalCondition(TerminalCondition condition);

        void ChangePort(IPort port);

        IPort Port { get; }

        TerminalCondition TerminalCondition { get; }

        void Calling(ICaller phone);

        void AceptCalling(ICaller caller);

        void StopCalling(ICaller secondCaller);

        void DropCalling(ICaller caller);
    }
}
