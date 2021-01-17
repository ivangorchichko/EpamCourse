using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IAts
    {
        ICallConnections CallConnections { get; }

        ITerminalService TerminalService { get; }

        ICaller CreateContract(int callerNumber, IUiManager uiManager);
    }
}
