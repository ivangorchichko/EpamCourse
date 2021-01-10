using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.ATS.Contracts;

namespace task3EPAMCourse.Billing.Contracts
{
    public interface IBilling
    {
        void StartConnecting(ICaller caller1, ICaller caller2);
        void StopConnecting(ICaller caller1, ICaller caller2);
    }
}
