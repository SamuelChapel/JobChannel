namespace JobChannel.Domain.Base
{
    public abstract record BaseEntity<TId>
    {
        public TId Id { get; }

        protected BaseEntity(TId id)
        {
            Id = id;
        }
    }
}
