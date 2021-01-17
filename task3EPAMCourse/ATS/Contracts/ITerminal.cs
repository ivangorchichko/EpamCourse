using System;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;
using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface ITerminal
    {
        event EventHandler<TerminalConnections> Call;

        event EventHandler<TerminalConnections> AcceptCall;

        event EventHandler<TerminalConnections> StopCall;

        event EventHandler<TerminalConnections> DropCall;

        event EventHandler<PortCondition> ChangePortCondition;

        string Number { get; }

        IPort Port { get; }

        TerminalCondition TerminalCondition { get; }

        void ChangeTerminalCondition(TerminalCondition condition);

        void ChangePort(IPort port);

        void ChangingPortCondition(PortCondition condition);

        TerminalConnections Calling(ICaller phone);

        TerminalConnections AcceptCalling(ICaller caller);

        TerminalConnections StopCalling(ICaller secondCaller);

        TerminalConnections DropCalling(ICaller caller);

        void UnSubscribeEvents();
    }
}
