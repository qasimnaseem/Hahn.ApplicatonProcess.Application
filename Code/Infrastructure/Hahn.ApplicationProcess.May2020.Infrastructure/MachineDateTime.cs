using Hahn.ApplicationProcess.May2020.Domain.Interfaces;
using System;

namespace Hahn.ApplicationProcess.May2020.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}
