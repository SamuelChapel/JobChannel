using JobChannel.Domain.Exceptions.Base;

namespace JobChannel.Domain.Exceptions
{
    public class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(int id) : base($"le daprtement avec l'id {id} n'existe pas")
        {
        }
    }
}
