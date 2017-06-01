using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForITreeHierarchy
    {
        /// <summary>
        ///     Returns this ITreeHierarchy as a flattened enumerable
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<TTreeHierarchy> Flatten<TTreeHierarchy>([NotNull] this TTreeHierarchy @this)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            yield return @this;
            foreach (TTreeHierarchy element in @this.Children.SelectMany(child => child.Flatten()))
            {
                yield return element;
            }
        }

        /// <summary>
        ///     Returns true wether <paramref name="parent" /> is in the parent chain of @this.
        ///     If <paramref name="this" /> is equal to <paramref name="parent" />, this method returns false.
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool IsChildOf<TTreeHierarchy>([NotNull] this TTreeHierarchy @this, [NotNull] TTreeHierarchy parent)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            return @this.Parent != null
                   && (@this.Parent.Id == parent.Id || @this.Parent.IsChildOf(parent));
        }

        /// <summary>
        ///     Returns true wether <paramref name="parentId" /> is in the parent chain of @this.
        ///     If <paramref name="this" /> is equal to <paramref name="parentId" />, this method returns false.
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static bool IsChildOf<TTreeHierarchy>([CanBeNull] this TTreeHierarchy @this, int parentId)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            if (@this == null)
            {
                return false;
            }
            return @this.ParentId.HasValue
                   && (@this.ParentId.Value == parentId || @this.Parent.IsChildOf(parentId));
        }

        /// <summary>
        ///     Returns true wether <paramref name="this" /> is in the parent chain of <paramref name="child" />.
        ///     If <paramref name="this" /> is equal to <paramref name="child" />, this method returns false.
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static bool IsParentOf<TTreeHierarchy>([NotNull] this TTreeHierarchy @this, [NotNull] TTreeHierarchy child)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            return @this.HasChildren()
                   && (@this.Children.Any(c => c.Id == child.Id) || @this.Children.Any(c => c.IsParentOf(child)));
        }

        /// <summary>
        ///     Returns true wether <paramref name="this" /> is in the parent chain of <paramref name="childId" />.
        ///     If <paramref name="this" /> is equal to <paramref name="childId" />, this method returns false.
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public static bool IsParentOf<TTreeHierarchy>([NotNull] this TTreeHierarchy @this, int childId)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            return @this.HasChildren()
                   && (@this.Children.Any(c => c.Id == childId) || @this.Children.Any(c => c.IsParentOf(childId)));
        }

        /// <summary>
        ///     Returns true if <paramref name="this" /> has children
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool HasChildren<TTreeHierarchy>([NotNull] this TTreeHierarchy @this)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            return @this.Children != null && @this.Children.Any();
        }

        /// <summary>
        ///     Returns the parents of this <see cref="TTreeHierarchy"/>
        /// </summary>
        /// <typeparam name="TTreeHierarchy"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<TTreeHierarchy> Parents<TTreeHierarchy>([NotNull] this TTreeHierarchy @this)
            where TTreeHierarchy : class, IIdentifiable, ITreeHierarchy<TTreeHierarchy>
        {
            if (@this.Parent == null)
                yield break;

            yield return @this.Parent;
            foreach (var p in @this.Parent.Parents())
            {
                yield return p;
            }
        }
    }
}
