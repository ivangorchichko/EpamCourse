using System.Collections.Generic;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Model;

namespace Task3EPAMCourse.ATS.Service
{
    public class CallConnections : ICallConnections
    {
        public IList<TerminalConnections> InWaitingConnectionCollection { get; } = new List<TerminalConnections>();

        public IList<TerminalConnections> InJoinedConnectionCollection { get; } = new List<TerminalConnections>();

        public void AddInWaitingCollection(TerminalConnections connection)
        {
            InWaitingConnectionCollection.Add(connection);
        }

        public void AddInJoinedCollection(TerminalConnections connection)
        {
            InJoinedConnectionCollection.Add(connection);
        }

        public void RemoveFromWaitingCollection(TerminalConnections connection)
        {
            InWaitingConnectionCollection.Remove(connection);
        }

        public void RemoveFromJoinedCollection(TerminalConnections connection)
        {
            InJoinedConnectionCollection.Remove(connection);
        }
    }
}
