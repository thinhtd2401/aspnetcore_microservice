using Contracts.Domains.Interfaces;

namespace Contracts.Domains;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public virtual TKey Id { get; set; }
}