using System.Collections.Generic;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Service
{
    public class CallService : ICallService
    {
        public IList<Connections> InWaitingConnectionCollection { get; } = new List<Connections>();
        public IList<Connections> InJoinedConnectionCollection { get; } = new List<Connections>();

        public void AddInWaitingCollection(Connections connection)
        {
            InWaitingConnectionCollection.Add(connection);
        }
        public void AddInJoinedCollection(Connections connection)
        {
            InJoinedConnectionCollection.Add(connection);
        }
        public void RemoveFromWaitingCollection(Connections connection)
        {
            InWaitingConnectionCollection.Remove(connection);
        }
        public void RemoveFromJoinedCollection(Connections connection)
        {
            InJoinedConnectionCollection.Remove(connection);
        }
    }
}
