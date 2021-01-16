using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.Billing.Contracts
{
    public interface IBilling
    {
        IEnumerable<CallInfo> GetCalls();

        IEnumerable<CallInfo> GetUserCalls(ICaller caller);

        IEnumerable<CallInfo> GetUserCallsOrderedByDuration(ICaller caller);

        IEnumerable<CallInfo> GetUserCallsOrderedByCost(ICaller caller);

        IEnumerable<CallInfo> GetUserCallsOrderedByCallers(ICaller caller);
    }
}
