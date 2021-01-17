using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IAts
    {
        IPortsService PortsService { get; }

        ICallConnections CallConnections { get; }

        ITerminalsService TerminalsService { get; }

        ICaller CreateContract(int callerNumber);
    }
}
