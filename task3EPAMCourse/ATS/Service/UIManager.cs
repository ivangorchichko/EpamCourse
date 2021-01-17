using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Service
{
    public class UIManager : IUIManager
    {
        public void GetInfoOnCreateContract(ICaller caller = null)
        {
            if (caller != null)
            {
                Console.WriteLine($"New contract is created! User {caller.CallerNumber}");
            }
            else Console.WriteLine("No more terminals or ports, caller set as null");
        }

        public void GetInfoTerminalOperation(ITerminal firstCaller, ITerminal secondCaller, TerminalOperations terminalOperations, TerminalConnectionsEventArgs connection = null, PortCondition portCondition = PortCondition.Free)
        {
            switch (terminalOperations)
            {
                case TerminalOperations.Calling:
                    {
                        if (portCondition == PortCondition.Free)
                        {
                            Console.WriteLine($"Terminal {firstCaller.Number} calls to {secondCaller.Number}");
                        }
                        else
                            Console.WriteLine($"Terminal {firstCaller.Number} can not call to {secondCaller.Number} cause them in call orr port is off");
                        break;
                    }
                case TerminalOperations.Acepting:
                    {
                        if (connection != null)
                        {
                            Console.WriteLine($"Terminal {firstCaller.Number} acept call with {secondCaller.Number}");
                        }
                        else
                            Console.WriteLine($"Terminal {firstCaller.Number} can not acept calling {secondCaller.Number} cause them dont call");
                        break;
                    }
                case TerminalOperations.Droping:
                    {
                        if (connection != null)
                        {
                            Console.WriteLine($"Terminal {firstCaller.Number} drop calling with {secondCaller.Number}");
                        }
                        else
                            Console.WriteLine($"Terminal {firstCaller.Number} can not drop calling {secondCaller.Number} cause caller dont call");
                        break;
                    }
                case TerminalOperations.Stoping:
                    {
                        if (connection != null)
                        {
                            Console.WriteLine($"Terminal {firstCaller.Number} stop calling with {secondCaller.Number}");
                        }
                        else
                            Console.WriteLine($"Terminal {firstCaller.Number} not in calling with {secondCaller.Number}");
                        break;
                    }
            }
        }
        //public void GetInfoWhenTerminalCalling(ITerminal caller, ICaller answerer, PortCondition portCondition = PortCondition.Free)
        //{
        //    if (portCondition == PortCondition.Free)
        //    {
        //        Console.WriteLine($"Terminal {caller.Number} calls to {answerer.Terminal.Number}");
        //    }
        //    else
        //        Console.WriteLine($"Terminal {caller.Number} can not call to {answerer.Terminal.Number} cause them in call orr port is off");
        //}

        //public void GetInfoWhenTerminalAcepting(ITerminal answerer, ICaller caller, TerminalConnectionsEventArgs connection = null)
        //{
        //    if (connection != null)
        //    {
        //        Console.WriteLine($"Terminal {answerer.Number} acept call with {caller.Terminal.Number}");
        //    }
        //    else
        //        Console.WriteLine($"Terminal {answerer.Number} can not acept calling {caller.Terminal.Number} cause them dont call");
        //}

        //public void GetInfoWhenTerminalStoping(ITerminal firstCaller, ITerminal secondCaller, TerminalConnectionsEventArgs connection = null)
        //{
        //    if (connection != null)
        //    {
        //        Console.WriteLine($"Terminal {firstCaller.Number} stop calling with {secondCaller.Number}");
        //    }
        //    else
        //        Console.WriteLine($"Terminal {firstCaller.Number} not in calling with {secondCaller.Number}");
        //}

        //public void GetInfoWhenTerminalDroping(ITerminal caller, ICaller answerer, TerminalConnectionsEventArgs connection = null)
        //{
        //    if (connection != null)
        //    {
        //        Console.WriteLine($"Terminal {caller.Number} drop calling with {answerer.Terminal.Number}");
        //    }
        //    else
        //        Console.WriteLine($"Terminal {caller.Number} can not drop calling {answerer.Terminal.Number} cause caller dont call");
        //}


    }
}
