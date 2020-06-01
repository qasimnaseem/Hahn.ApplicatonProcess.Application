using System;

namespace Hahn.ApplicationProcess.May2020.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity [{name}] having ID = {key} was not found.")
        {
        }
    }
}
