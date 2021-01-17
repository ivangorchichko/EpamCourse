using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IUIManager
    {
        void GetInfoOnCreateContract(ICaller caller = null);

        void GetInfoTerminalOperation(ITerminal firstCaller,
            ITerminal secondCaller,
            TerminalOperations terminalOperations, 
            TerminalConnectionsEventArgs connection = null, 
            PortCondition portCondition = PortCondition.Free);
    }
}
