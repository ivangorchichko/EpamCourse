using System.Collections.Generic;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.ATS.Service;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface IATS
    {
        ICallService CallService { get; }
        IPortService PortService { get; }
        ITerminalService TerminalService { get; }
        ICaller CreateContract(int callerNumber);
        void RegistrATSEvents();
    }
}
