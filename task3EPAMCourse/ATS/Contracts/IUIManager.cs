using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;
using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IUiManager
    {
        void GetInfoOnCreateContract(ICaller caller = null);

        void GetInfoTerminalOperation(
            TerminalConnections callersConnection,
            TerminalOperations terminalOperations,
            PortCondition portCondition = PortCondition.Free);

        void GetInfoIfPortConditionChanged(PortCondition condition);

        ICaller GetInfoIfContractCreated(ICaller caller);
    }
}
