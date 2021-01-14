using System;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ITerminal
    {
        event EventHandler<TerminalConnectionsEventArgs> Call;
        event EventHandler<TerminalConnectionsEventArgs> AceptCall;
        event EventHandler<TerminalConnectionsEventArgs> StopCall;
        event EventHandler<TerminalConnectionsEventArgs> DropCall;
        event EventHandler<PortCondition> ChangePortCondition;
        void ChangeTerminalCondition(TerminalCondition condition);

        void ChangePort(IPort port);

        IPort Port { get; }

        TerminalCondition TerminalCondition { get; }

        void Calling(ICaller phone);

        void AceptCalling(ICaller caller);

        void StopCalling(ICaller secondCaller);

        void DropCalling(ICaller caller);

        void ChangingPortCondition(PortCondition condition);
    }
}
