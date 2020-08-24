using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Testes.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Repositorio
{
    public class PessoaRepositorioTest : DatabaseFactory
    {
        public PessoaRepositorioTest()
        {
            DroparBaseDeDados();
            CriarBaseDeDadosETabelas();
        }

        [Fact]
        public void Salvar()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(pessoa.Nome.ToString(), retorno.Nome);
            Assert.Equal(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Fact]
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

            Assert.Equal(pessoa.Id, retorno.Id);
            Assert.Equal(pessoa.Nome.ToString(), retorno.Nome);
            Assert.Equal(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Fact]
        public void Deletar()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            repository.Deletar(2);

            List<PessoaQueryResult> retorno = repository.Listar();

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(pessoa0.Nome.ToString(), retorno[0].Nome);
            Assert.Equal(pessoa0.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.Equal(3, retorno[1].Id);
            Assert.Equal(pessoa2.Nome.ToString(), retorno[1].Nome);
            Assert.Equal(pessoa2.ImagemPerfil, retorno[1].ImagemPerfil);
        }

        [Fact]
        public void Obter()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            PessoaQueryResult retorno = repository.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(pessoa.Nome.ToString(), retorno.Nome);
            Assert.Equal(pessoa.ImagemPerfil, retorno.ImagemPerfil);
        }

        [Fact]
        public void Listar()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            List<PessoaQueryResult> retorno = repository.Listar();

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(pessoa0.Nome.ToString(), retorno[0].Nome);
            Assert.Equal(pessoa0.ImagemPerfil, retorno[0].ImagemPerfil);

            Assert.Equal(2, retorno[1].Id);
            Assert.Equal(pessoa1.Nome.ToString(), retorno[1].Nome);
            Assert.Equal(pessoa1.ImagemPerfil, retorno[1].ImagemPerfil);

            Assert.Equal(3, retorno[2].Id);
            Assert.Equal(pessoa2.Nome.ToString(), retorno[2].Nome);
            Assert.Equal(pessoa2.ImagemPerfil, retorno[2].ImagemPerfil);
        }

        [Fact]
        public void CheckId()
        {
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa);

            bool idExistente = repository.CheckId(1);
            bool idNaoExiste = repository.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Fact]
        public void LocalizarMaxId()
        {
            Pessoa pessoa0 = new Pessoa(0, new Texto("NomePessoa0", "Nome", 100), "ImagemPerfil0");
            Pessoa pessoa1 = new Pessoa(0, new Texto("NomePessoa1", "Nome", 100), "ImagemPerfil1");
            Pessoa pessoa2 = new Pessoa(0, new Texto("NomePessoa2", "Nome", 100), "ImagemPerfil2");

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            PessoaRepositorio repository = new PessoaRepositorio(mockOptions.Object);
            repository.Salvar(pessoa0);
            repository.Salvar(pessoa1);
            repository.Salvar(pessoa2);

            int maxId = repository.LocalizarMaxId();

            Assert.Equal(3, maxId);
        }
    }
}