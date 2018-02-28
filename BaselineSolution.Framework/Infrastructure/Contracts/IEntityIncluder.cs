using System.Linq;

namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    public interface IEntityIncluder<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Performs the inclusions on the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities on which to perform the inclusions</param>
        /// <returns>The entities that are now marked with the properties that need to be eagerly loaded</returns>
        IQueryable<TEntity> AddInclusions(IQueryable<TEntity> entities);

        IEntityIncluder<TCast> Cast<TCast>() where TCast : class, TEntity;
    }
}
