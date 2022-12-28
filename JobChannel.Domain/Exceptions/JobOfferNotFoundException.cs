using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class JobOfferNotFoundException : NotFoundException
    {
        public JobOfferNotFoundException(int id) : base($"l'annonce avec l'id {id} n'existe pas")
        {
        }
    }
}
