using System.Collections.Generic;
using task3EPAMCourse.Billing.Enums;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.Billing.Contracts
{
    public interface IBilling
    {
        IEnumerable<CallInfo> GetCalls();

        IEnumerable<CallInfo> GetUserCallsOrderedBy(ICaller caller, OrderSequenceType orderType);
    }
}
