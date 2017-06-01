using System;

namespace BaselineSolution.Framework.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAuthenticatedAttribute : Attribute
    {
    }
}
