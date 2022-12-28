using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class JobNotFoundException : NotFoundException
    {
        public JobNotFoundException(int id) : base ($"Le job avec l'id {id} n'existe pas") { }
    }
}
