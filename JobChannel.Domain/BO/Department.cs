using System;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Region Region { get; set; }

        public Department() : base(-1)
        {
            Name = String.Empty;
            Code = String.Empty;
            Region = new Region();
        }

        public Department(
            int id,
            string name,
            string codeRome,
            Region region) : base(id)
        {
            Name = name;
            Code = codeRome;
            Region = region;
        }
    }
}
