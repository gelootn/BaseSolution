namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    /// <summary>
    /// Interface for entities that are owned (multi-tenancy)
    /// </summary>
    public interface IOwnable : IIdentifiable
    {
        int? OwnerId { get; set; }
    }
}
