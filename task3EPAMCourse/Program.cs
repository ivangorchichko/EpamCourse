using System.Collections.Generic;
using System.Threading;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Contracts;
using task3EPAMCourse.Billing.Model;


namespace task3EPAMCourse
{
    class Program
    {
        private static readonly IATS _aTS = new AutoTelephoneStation();
        private static readonly IUIManager _uIManager = new UIManager();
        private static readonly IList<ICaller> _callers = new List<ICaller>();
        private static readonly BillingSystem _billingSystem = new BillingSystem(_aTS);
        static void Main(string[] args)
        {
            _callers.Add(_aTS.CreateContract(1));
            _callers.Add(_aTS.CreateContract(2));
            _callers.Add(_aTS.CreateContract(3));
            _callers.Add(_aTS.CreateContract(4));

            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AceptCalling(_callers[0]);
            Thread.Sleep(5000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            

            _callers[3].Terminal.Calling(_callers[2]);
            Thread.Sleep(6000);
            _callers[2].Terminal.DropCalling(_callers[3]);

            
            _callers[2].Terminal.Calling(_callers[0]);
            Thread.Sleep(5000);
            _callers[1].Terminal.DropCalling(_callers[0]);
            _callers[2].Terminal.Calling(_callers[1]);
            Thread.Sleep(5000);
            _callers[1].Terminal.AceptCalling(_callers[2]);
            _callers[0].Terminal.DropCalling(_callers[1]);

            _callers[1].Terminal.AceptCalling(_callers[0]);
            _callers[0].Terminal.StopCalling(_callers[1]);

            foreach (var call in _billingSystem.GetCalls())
            {
                System.Console.WriteLine(call.ToString());
            }
        }
    }
}
