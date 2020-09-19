namespace Order.Domain.Entities.Base
{
    public interface IEntityBase<out TId>
    {
        TId Id { get; }
    }
}