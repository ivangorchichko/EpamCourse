using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IUiManager
    {
        void GetInfoOnCreateContract(ICaller caller = null);

        void GetInfoTerminalOperation(ITerminal firstCaller,
            ITerminal secondCaller,
            TerminalOperations terminalOperations, 
            TerminalConnectionsEventArgs connection = null, 
            PortCondition portCondition = PortCondition.Free);
    }
}
