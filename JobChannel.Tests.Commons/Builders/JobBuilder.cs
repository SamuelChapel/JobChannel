using JobChannel.Domain.BO;

namespace JobChannel.Tests.Commons.Builders
{
    public class JobBuilder
    {
        private int _id = 1;
        private string _name = "fooName";
        private string _codeRome = "fooCodeRome";

        public static JobBuilder AJob => new();

        public JobBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public JobBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public JobBuilder WithCodeRome(string codeRome) 
        {
            _codeRome = codeRome;
            return this;
        }

        public Job Build()
        {
            return new Job()
            {
                Id = _id,
                Name= _name,
                CodeRome = _codeRome
            };
        }
    }
}
