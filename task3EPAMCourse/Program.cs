using System.Collections.Generic;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.ATS.Service;

namespace task3EPAMCourse
{
    class Program
    {
        private static readonly IATS _aTS = new AutoTelephoneStation();
        private static readonly IUIManager _uIManager = new UIManager();
        private static readonly IList<ICaller> _callers = new List<ICaller>();
        static void Main(string[] args)
        {
            _callers.Add(_aTS.CreateContract(_uIManager.GetUserId()));
            _callers.Add(_aTS.CreateContract(_uIManager.GetUserId()));

            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AceptCalling(_callers[0]);
            _callers[0].Terminal.StopCalling(_callers[1]);

        }
    }
}
