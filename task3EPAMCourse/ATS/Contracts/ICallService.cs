using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ICallService
    {
        IList<TerminalConnectionsEventArgs> InWaitingConnectionCollection { get; }
        IList<TerminalConnectionsEventArgs> InJoinedConnectionCollection { get; }
        void AddInWaitingCollection(TerminalConnectionsEventArgs connection);
        void AddInJoinedCollection(TerminalConnectionsEventArgs connection);
        void RemoveFromWaitingCollection(TerminalConnectionsEventArgs connection);
        void RemoveFromJoinedCollection(TerminalConnectionsEventArgs connection);
    }
}
