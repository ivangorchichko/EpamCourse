using System.Collections.Generic;
using Task3EPAMCourse.ATS.Model;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface ICallConnections
    {
        IList<TerminalConnections> InWaitingConnectionCollection { get; }

        IList<TerminalConnections> InJoinedConnectionCollection { get; }

        void AddInWaitingCollection(TerminalConnections connection);

        void AddInJoinedCollection(TerminalConnections connection);

        void RemoveFromWaitingCollection(TerminalConnections connection);

        void RemoveFromJoinedCollection(TerminalConnections connection);
    }
}
