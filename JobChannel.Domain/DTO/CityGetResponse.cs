namespace JobChannel.Domain.DTO
{
    public record CityGetResponse
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Id { get; set; }
    }
}
