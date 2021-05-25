using ControleDespesas.Infra.Data.Repositories;
using ControleDespesas.Infra.Interfaces.Repositories;
using ControleDespesas.Test.AppConfigurations.Base;
using NUnit.Framework;

namespace ControleDespesas.Test.HealthCheck.Repositories
{
    public class HealthCheckRepositoryTest : DatabaseTest
    {
        private readonly IHealthCheckRepository _repository;

        public HealthCheckRepositoryTest()
        {
            CriarBaseDeDadosETabelas();

            _repository = new HealthCheckRepository(MockSettingsInfraData);
        }

        [SetUp]
        public void Setup() => CriarBaseDeDadosETabelas();

        [Test]
        public void Salvar()
        {
            _repository.Verificar();
            Assert.True(true);
        }

        [TearDown]
        public void TearDown() => DroparTabelas();
    }
}