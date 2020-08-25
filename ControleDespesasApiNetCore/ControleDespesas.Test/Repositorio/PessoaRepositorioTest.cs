using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesasApiNetCore.TestNUnit.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ControleDespesasApiNetCore.TestNUnit.Repositorio
{
    public class PessoaRepositorioTest : DatabaseFactory
    {
        [SetUp]
        public void Setup()
        {
            CriarBaseDeDadosETabelas();
        }

        [Test]
        public void Salvar()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(1, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Test]
        public void Atualizar()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            pessoa = new Pessoa(1, new Texto("NomePessoa - Editada", "Nome", 100), "ImagemPerfil - Editado");
            repository.Atualizar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.AreEqual(pessoa.Id, retorno.Id);
            Assert.AreEqual(pessoa.Nome.ToString(), retorno.Nome);
            Assert.AreEqual(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [TearDown]
        public void TearDown()
        {
            DroparBaseDeDados();
        }
    }
}