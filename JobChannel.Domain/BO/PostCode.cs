namespace JobChannel.Domain.BO
{
    public record PostCode
    {
        public string Postcode { get; set; }

        public PostCode() => Postcode = string.Empty;

        public PostCode(string postcode) => Postcode = postcode;
    }
}
