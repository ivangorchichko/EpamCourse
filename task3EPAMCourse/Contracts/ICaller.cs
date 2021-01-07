using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Enums;

namespace task3EPAMCourse.Contracts
{
    public interface ICaller
    {
         ITerminal Terminal { get; }
         void ChangePortCondition(PortCondition condition);
    }
}
