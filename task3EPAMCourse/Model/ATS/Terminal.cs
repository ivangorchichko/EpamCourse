using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Contracts;

namespace task3EPAMCourse.Model
{
    public class Terminal
    {
        public string Number { get; }
        public IPort Port { get; }

        public Terminal(string number, IPort port)
        {
            Number = number;
            Port = port;
        }
    }
}
