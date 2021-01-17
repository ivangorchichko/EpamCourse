using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IAts
    {
        ICallService CallService { get; }

        IPortService PortService { get; }

        ITerminalService TerminalService { get; }

        ICaller CreateContract(int callerNumber, IUiManager uiManager);
    }
}
