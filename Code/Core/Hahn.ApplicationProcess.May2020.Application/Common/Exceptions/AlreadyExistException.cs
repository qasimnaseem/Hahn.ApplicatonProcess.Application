using System;

namespace Hahn.ApplicationProcess.May2020.Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
