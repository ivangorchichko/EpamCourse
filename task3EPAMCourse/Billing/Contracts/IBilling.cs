using System.Collections.Generic;
using Task3EPAMCourse.Billing.Enums;
using Task3EPAMCourse.Billing.Model;

namespace Task3EPAMCourse.Billing.Contracts
{
    public interface IBilling
    {
        IEnumerable<CallInfo> GetCalls();

        IEnumerable<CallInfo> GetUserCallsOrderedBy(ICaller caller, OrderSequenceType orderType);
    }
}
