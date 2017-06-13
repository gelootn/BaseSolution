using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Domain.Security
{
    public class RoleRight : Entity
    {
        /// <summary>
        /// Gets or Sets the RoleId
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets the Role
        /// </summary>
        public virtual Role Role { get; set; }


        /// <summary>
        /// Gets or Sets the RightId
        /// </summary>
        public virtual int RightId { get; set; }

        /// <summary>
        /// Gets or Sets the Right
        /// </summary>
        public virtual Right Right { get; set; }

        /// <summary>
        /// Gets or Sets the Allow
        /// </summary>
        public virtual bool? Allow { get; set; }

    }
}
