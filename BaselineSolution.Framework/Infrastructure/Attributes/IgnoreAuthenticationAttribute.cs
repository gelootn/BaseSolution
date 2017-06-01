using System;

namespace BaselineSolution.Framework.Infrastructure.Attributes
{
    /// <summary>
    /// Indicates that this action will not be configurable in the security system (rights system). 
    /// (It can be useful to hide actions that for example should always be allowed/disallowed)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class IgnoreAuthenticationAttribute : Attribute
    {
    }
}
