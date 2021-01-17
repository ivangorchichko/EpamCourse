namespace Task3EPAMCourse.ATS.Contracts
{
    public interface IPortService
    {
        IPort GetFreePort();

        void CreateNewPort(IPort newPort);
    }
}
