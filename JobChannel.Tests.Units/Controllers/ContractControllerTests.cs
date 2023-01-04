using System.Collections.Generic;
using System.Linq;
using JobChannel.API.Controllers.Contracts;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.Domain.BO;
using Moq;
using Xunit;

namespace JobChannel.Tests.Units.Controllers
{
    public class ContractControllerTests
    {
        [Fact]
        public async void ShouldReturnContracts_WhenGetAll()
        {
            // Arrange
            var contractServiceMock = new Mock<IContractService>();
            var contracts = new List<Contract>()
            {
                new Contract{ Id = 1, Code = "fooCode", Name= "fooName"},
                new Contract{ Id = 2, Code = "falseCode", Name = "falseName"}
            };
            contractServiceMock.Setup(c => c.GetAll().Result)
                               .Returns(contracts);
            var contractController = new ContractController(contractServiceMock.Object);

            // Act
            var result = await contractController.GetAll();

            // Assert
            Assert.True(result.Count() == 2);
            Assert.Collection(result,
                    r => Assert.Equal(r, contracts[0]),
                    r => Assert.Equal(r, contracts[1])
                );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async void ShouldReturnContract_WhenGetById(int id)
        {
            //Arrange
            var contractServiceMock = new Mock<IContractService>();
            var contract = new Contract { Id = id, Name = "Stage", Code = "sta"};
            contractServiceMock.Setup(j => j.GetById(id).Result)
                               .Returns(contract);

            ContractController contractController = new ContractController(contractServiceMock.Object);

            //Act
            var result = await contractController.GetById(id);

            //Assert
            Assert.Equal(contract, result);
        }
    }
}
