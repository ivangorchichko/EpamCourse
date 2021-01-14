using System;
using System.Collections.Generic;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Service
{
    public class CallService : ICallService
    {
        public IList<TerminalConnectionsEventArgs> InWaitingConnectionCollection { get; } = new List<TerminalConnectionsEventArgs>();
        public IList<TerminalConnectionsEventArgs> InJoinedConnectionCollection { get; } = new List<TerminalConnectionsEventArgs>();

        public void AddInWaitingCollection(TerminalConnectionsEventArgs connection)
        {
            InWaitingConnectionCollection.Add(connection);
        }
        public void AddInJoinedCollection(TerminalConnectionsEventArgs connection)
        {
            InJoinedConnectionCollection.Add(connection);
        }
        public void RemoveFromWaitingCollection(TerminalConnectionsEventArgs connection)
        {
            InWaitingConnectionCollection.Remove(connection);
        }
        public void RemoveFromJoinedCollection(TerminalConnectionsEventArgs connection)
        {
            InJoinedConnectionCollection.Remove(connection);
        }
    }
}
