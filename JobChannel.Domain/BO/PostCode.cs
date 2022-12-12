using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChannel.Domain.BO
{
    public record PostCode
    {
        public string Postcode { get; set; }

        public PostCode() => Postcode = string.Empty;

        public PostCode(string postcode) => Postcode = postcode;
    }
}
