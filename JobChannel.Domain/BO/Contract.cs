using System;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record Contract : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Contract() : base(-1)
        {
            Name = String.Empty;
            Code = String.Empty;
        }

        public Contract(int id, string name, string code) : base(id)
        {
            Name = name;
            Code = code;
        }
    }
}
