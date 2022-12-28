using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class ContractNotFoundException : NotFoundException
    {
        public ContractNotFoundException(int id) : base($"le contrat avec l'id {id} n'existe pas")
        {
        }
    }
}
