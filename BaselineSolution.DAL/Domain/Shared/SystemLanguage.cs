using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Domain.Shared
{
    public class SystemLanguage : Entity
    {
        /// <summary>
        /// Gets or Sets the Culture
        /// </summary>
        public virtual string Culture { get; set; }

        /// <summary>
        /// Gets or Sets the Label
        /// </summary>
        public virtual string Label { get; set; }
    }
}
