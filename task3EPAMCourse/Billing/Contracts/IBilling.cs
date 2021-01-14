using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.Billing.Contracts
{
    public interface IBilling
    {
        IList<CallInfo> GetCalls();
    }
}
