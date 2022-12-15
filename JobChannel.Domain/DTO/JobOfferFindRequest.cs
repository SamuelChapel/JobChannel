namespace JobChannel.Domain.DTO
{
    public record JobOfferFindRequest
    {
        public int? Id_Region { get; set; }
        public int? Id_Department { get; set; }
        public int? Id_City { get; set; }
        public int? Id_Job { get; set; }
        public int? Id_Contract { get; set; }
        public string? SearchString { get; set; }
    }
}
