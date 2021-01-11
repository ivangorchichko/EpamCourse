using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ICallService
    {
        IList<Connections> InWaitingConnectionCollection { get; }
        IList<Connections> InJoinedConnectionCollection { get; }
        void AddInWaitingCollection(Connections connection);
        void AddInJoinedCollection(Connections connection);
        void RemoveFromWaitingCollection(Connections connection);
        void RemoveFromJoinedCollection(Connections connection);
    }
}
