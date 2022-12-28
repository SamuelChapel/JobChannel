using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class RegionNotFoundException : NotFoundException
    {
        public RegionNotFoundException(int id) : base($"la region avec l'id {id} n'existe pas")
        {
        }
    }
}
