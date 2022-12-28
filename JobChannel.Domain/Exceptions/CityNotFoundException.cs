using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class CityNotFoundException : NotFoundException
    {
        public CityNotFoundException(int id) : base($"la ville avec l'id {id} n'existe pas")
        {
        }
    }
}
