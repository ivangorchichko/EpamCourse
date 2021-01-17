using System;
using System.Collections.Generic;
using System.Threading;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Enums;
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
            CreateContracts();
            CreateConnections();
            ShowCallerInfoCollection();
        }

        private static void CreateContracts()
        {
            _callers.Add(_aTS.CreateContract(1, _uIManager));
            _callers.Add(_aTS.CreateContract(2, _uIManager));
            _callers.Add(_aTS.CreateContract(3, _uIManager));
            _callers.Add(_aTS.CreateContract(4, _uIManager));
            _callers.Add(_aTS.CreateContract(5, _uIManager));
            Console.WriteLine();
        }

        private static void CreateConnections()
        {
            //good conect
            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AceptCalling(_callers[0]);
            Thread.Sleep(5000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();

            //good conect
            _callers[3].Terminal.Calling(_callers[2]);
            Thread.Sleep(6000);
            _callers[2].Terminal.DropCalling(_callers[3]);
            Console.WriteLine();

            //bad conect
            _callers[2].Terminal.StopCalling(_callers[0]);
            _callers[1].Terminal.DropCalling(_callers[0]);
            _callers[1].Terminal.AceptCalling(_callers[2]);
            Console.WriteLine();

            //good conect
            _callers[1].Terminal.Calling(_callers[3]);
            _callers[3].Terminal.AceptCalling(_callers[1]);
            Thread.Sleep(4000);
            _callers[1].Terminal.StopCalling(_callers[3]);
            Console.WriteLine();

            //good conect
            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AceptCalling(_callers[0]);
            Thread.Sleep(8000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();

            //good conect
            _callers[2].Terminal.Calling(_callers[3]);
            _callers[2].Terminal.AceptCalling(_callers[3]);
            _callers[3].Terminal.AceptCalling(_callers[2]);
            _callers[0].Terminal.Calling(_callers[2]);
            Thread.Sleep(6000);
            _callers[3].Terminal.StopCalling(_callers[2]);
            Console.WriteLine();

            //good conect
            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AceptCalling(_callers[0]);
            Thread.Sleep(7000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();
        }

        private static void ShowCallerInfoCollection()
        {
            Console.WriteLine("All CallsInfo with old\n");
            foreach (var callInfo in _billingSystem.GetCalls())
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }

            var firstUserCallInfoCollection = _billingSystem.GetUserCallsOrderedBy(_callers[0], OrderSequenceType.Cost);
            Console.WriteLine("User " + _callers[0].CallerNumber + "  Calls ordered by cost\n");
            foreach (var callinfo in firstUserCallInfoCollection)
            {
                Console.WriteLine(callinfo.ToString());
                Console.WriteLine();
            }

            var secondUserCallInfoCollection = _billingSystem.GetUserCallsOrderedBy(_callers[1], OrderSequenceType.Duration);
            Console.WriteLine("User " + _callers[1].CallerNumber + "  Calls ordered by duration\n");
            foreach (var callinfo in secondUserCallInfoCollection)
            {
                Console.WriteLine(callinfo.ToString());
                Console.WriteLine();
            }

            var thirdUserCallInfoCollection = _billingSystem.GetUserCallsOrderedBy(_callers[2], OrderSequenceType.Callers);
            Console.WriteLine("User " + _callers[2].CallerNumber + "  Calls ordered by callers\n");
            foreach (var callinfo in thirdUserCallInfoCollection)
            {
                Console.WriteLine(callinfo.ToString());
                Console.WriteLine();
            }

            var fourthUserCallInfoCollection = _billingSystem.GetUserCallsOrderedBy(_callers[3], OrderSequenceType.None);
            Console.WriteLine("User " + _callers[3].CallerNumber + "  Calls\n");
            foreach (var callinfo in fourthUserCallInfoCollection)
            {
                Console.WriteLine(callinfo.ToString());
                Console.WriteLine();
            }
        }
    }
}
