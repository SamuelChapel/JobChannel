using JobChannel.BLL.Services.Base;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.JobServices
{
    public interface IJobService : IGenericReadService<Job, int>
    {
    }
}