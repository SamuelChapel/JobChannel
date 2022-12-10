using System;
using System.Collections.Generic;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record City : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Department Department { get; set; }
        public IEnumerable<string> PostCodes { get; set; }
        public int Population { get; set; }

        public City() : base(-1)
        {
            Name = String.Empty;
            Code = String.Empty;
            Department = new Department();
            PostCodes= new List<string>();
            Population = 0;
        }

        public City(
            int id,
            string name,
            string codeRome,
            Department department,
            IEnumerable<string> postCodes,
            int population) : base(id)
        {
            Name = name;
            Code = codeRome;
            Department = department;
            PostCodes = postCodes;
            Population = population;
        }
    }
}
