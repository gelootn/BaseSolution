using System.Collections.Generic;

namespace BaselineSolution.Framework.Utilities
{
    public sealed class EnvironmentInfo
    {
        // ReSharper disable InconsistentNaming cannot rename current to Current as it would conflict with the public singleton property
        private static readonly EnvironmentInfo current = new EnvironmentInfo();
        // ReSharper restore InconsistentNaming

        static EnvironmentInfo() { }

        private EnvironmentInfo() { }

        /// <summary>
        ///     Gets the current environment info
        /// </summary>
        public static EnvironmentInfo Current
        {
            get { return current; }
        }

        /// <summary>
        ///     Gets or sets the culture that should be applied to translatable fields, etc.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        ///     Gets or sets the active cultures
        /// </summary>
        public IEnumerable<string> ActiveCultures { get; set; }
    }
}
