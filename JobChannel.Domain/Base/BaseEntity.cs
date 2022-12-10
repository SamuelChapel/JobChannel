namespace JobChannel.Domain.Base
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; }

        protected BaseEntity(TId id)
        {
            Id = id;
        }
    }
}
