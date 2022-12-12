namespace JobChannel.Domain.Base
{
    public abstract record BaseEntity<TId>
    {
        public TId Id { get; set; }

        protected BaseEntity(TId id) => Id = id;
    }
}
