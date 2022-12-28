using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.Domain.Base;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.Base
{
    public interface IGenericReadController<T, Tid, Trequest> where Trequest : IRequest where T : BaseEntity<Tid> where Tid : struct
    {
        Task<T> GetById(Tid id);

        Task<IEnumerable<T>> Find(Trequest request, IValidator<Trequest> validator);
    }
}
