namespace JobChannel.Domain.DTO
{
    public record DepartmentGetResponse
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Id { get; set; }
    }
}
