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

        //void GetInfoWhenTerminalCalling(ITerminal caller, ICaller answerer, PortCondition portCondition = PortCondition.Free);

        //void GetInfoWhenTerminalAcepting(ITerminal answerer, ICaller caller, TerminalConnectionsEventArgs connection = null);

        //void GetInfoWhenTerminalStoping(ITerminal firstCaller, ITerminal secondCaller, TerminalConnectionsEventArgs connection = null);

        //void GetInfoWhenTerminalDroping(ITerminal caller, ICaller answerer, TerminalConnectionsEventArgs connection = null);
    }
}
