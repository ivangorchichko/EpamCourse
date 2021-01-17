namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IPortsService
    {
        IPort GetFreePort();

        void CreateNewPort(IPort newPort);
    }
}
