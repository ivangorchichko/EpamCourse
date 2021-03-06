﻿using System;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.Billing.Enums;

namespace Task3EPAMCourse.Billing.Model
{
    public class CallInfo
    {
        public CallInfo()
        {
        }

        public CallInfo(CallInfo callInfo)
        {
            User = callInfo.User;
            From = callInfo.From;
            To = callInfo.To;
            DateTimeStart = callInfo.DateTimeStart;
            Duration = callInfo.Duration;
            CallType = callInfo.CallType;
            Cost = callInfo.Cost;
        }

        public ITerminal User { get; set; }

        public ITerminal From { get; set; }

        public ITerminal To { get; set; }

        public DateTime DateTimeStart { get; set; }

        public TimeSpan Duration { get; set; }

        public CallType CallType { get; set; }

        public double Cost { get; set; }

        public override string ToString()
        {
            return $"User {User.Number}\n" +
                $"From: {From.Number}\t" +
                $"To: {To.Number}\n" +
                $"Started at: {DateTimeStart:F}\t" +
                $"Duration: {Duration:hh\\:mm\\:ss}\n" +
                $"Type: {CallType}\t" +
                $"Cost: {Cost:F2}";
        }
    }
}
