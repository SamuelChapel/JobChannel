using JobChannel.Domain.BO;
using System;

namespace JobChannel.Domain.DTO
{
    public record JobOfferCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Url { get; set; }
        public string Salary { get; set; }
        public string Experience { get; set; }
        public int JobId { get; set; }
        public int ContractId { get; set; }
        public int CityId { get; set; }
        public int CompanyId { get; set; }

        public JobOfferCreateRequest(
            string title,
            string description,
            DateTime publicationDate,
            DateTime modificationDate,
            string url,
            string salary,
            string experience,
            int jobId,
            int contractId,
            int cityId,
            int companyId)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ModificationDate = modificationDate;
            Url = url;
            Salary = salary;
            Experience = experience;
            JobId = jobId;
            ContractId = contractId;
            CityId = cityId;
            CompanyId = companyId;
        }
    }
}
