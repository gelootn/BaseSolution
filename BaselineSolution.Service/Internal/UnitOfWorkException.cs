using System;

namespace BaselineSolution.Service.Internal
{
    public class UnitOfWorkException : Exception
    {
        public UnitOfWorkException(string unitOfWorkNotSet) : base(unitOfWorkNotSet)
        {

        }
    }
}