using System;
using JobChannel.Domain.Base;

namespace JobChannel.Domain.BO
{
    public record JobOffer : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Url { get; set; }
        public string Salary { get; set; }
        public string Experience { get; set; }
        public Job Job { get; set; }
        public Contract Contract { get; set; }
        public City City { get; set; }
        public Company Company { get; set; }

        public JobOffer() : base(-1)
        {
            Title = String.Empty;
            Description = String.Empty;
            Url = String.Empty;
            Salary = String.Empty;
            Experience = String.Empty;
            Job = new Job();
            Contract = new Contract();
            City = new City();
            Company = new Company();
        }

        public JobOffer(
            int id,
            string title,
            string description,
            DateTime publicationDate,
            DateTime modificationDate,
            string url,
            string salary,
            string experience,
            Job job,
            Contract contract,
            City city,
            Company company) : base(id)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ModificationDate = modificationDate;
            Url = url;
            Salary = salary;
            Experience = experience;
            Job = job;
            Contract = contract;
            City = city;
            Company = company;
        }
    }
}
