namespace task3EPAMCourse.ATS.Contracts
{
    public interface IATS
    {
        ICallService CallService { get; }
        IPortService PortService { get; }
        ITerminalService TerminalService { get; }
        ICaller CreateContract(int callerNumber, IUIManager uiManager);
    }
}
