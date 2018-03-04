using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Infrastructure.Extentions
{
    internal static class ExtensionsForICollection
    {
        public static ICollection<TEntity> UpdateWith<TEntity, TBase>(this ICollection<TEntity> original,
            IEnumerable<TBase> updated, ITranslator<TBase, TEntity> translator)
            where TEntity : Entity, new()
            where TBase : BaseBo
        {
            List<TBase> updatedList = updated.ToList();

            // delete objects from original that are not in updated collection
            List<int> updatedIds = updatedList.Select(u => u.Id).ToList();
            List<TEntity> deletedEntities = original.Where(o => !updatedIds.Contains(o.Id)).ToList();
            foreach (TEntity entity in deletedEntities)
            {
                //original.Remove(entity);
                entity.Deleted = true;
            }

            // update objects in original that are also found in the updated collection
            foreach (TEntity entity in original.Where(o => updatedIds.Contains(o.Id)))
            {
                TBase entityDto = updatedList.Single(u => u.Id == entity.Id);
                entityDto.UpdateModel(entity, translator);
            }

            // add objects to original that are only found in the updated collection
            IEnumerable<TBase> newEntities = updatedList.Where(u => u.Id == 0);
            foreach (var entityDto in newEntities)
            {
                original.Add(entityDto.UpdateModel(new TEntity(), translator));
            }
            return original;
        }
    }
}
