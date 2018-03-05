namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    public interface ISelectable : IIdentifiable
    {
        /// <summary>
        ///     Gets or sets the key. The key should be unique across all instances of one type.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        ///     Gets or sets the name. 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is the default <see cref="ISelectable"/> for its type.
        /// </summary>
        bool IsDefault { get; set; }
    }
}
