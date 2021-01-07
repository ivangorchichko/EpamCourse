using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Model.BillimgSystem;

namespace task3EPAMCourse.Contracts
{
    public interface IATS
    {
        IEnumerable<ITerminal> Terminals { get; }
        Billing Billing { get; }

        void CreateContract(ref ICaller caller, int callerNumber, ITerminal terminal);

        void CallersConect(ICaller caller1, ICaller caller2);

        void StopCallersConect(ICaller caller1, ICaller caller2);

    }
}
