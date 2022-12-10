using System;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record Company : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Company() : base(-1)
        {
            Name = String.Empty;
            Description = String.Empty;
        }

        public Company(int id, string name, string codeRome) : base(id)
        {
            Name = name;
            Description = codeRome;
        }
    }
}
