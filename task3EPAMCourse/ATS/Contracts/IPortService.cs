using System.Collections.Generic;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IPortService
    {
        IPort GetFreePort();
        IEnumerable<IPort> GetPorts();
    }
}
