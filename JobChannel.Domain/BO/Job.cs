using System;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record Job : BaseEntity<int>
    {
        public string Name { get; set; }
        public string CodeRome { get; set; }

        public Job() : base(-1)
        {
            Name = String.Empty;
            CodeRome = String.Empty;
        }

        public Job(int id, string name, string codeRome) : base(id)
        {
            Name = name;
            CodeRome = codeRome;
        }
    }
}
