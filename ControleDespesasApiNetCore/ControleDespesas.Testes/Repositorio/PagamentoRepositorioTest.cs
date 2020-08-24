﻿using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pagamento;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Testes.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControleDespesas.Testes.Repositorio
{
    public class PagamentoRepositorioTest : DatabaseFactory
    {
        public PagamentoRepositorioTest()
        {
            DroparBaseDeDados();
            CriarBaseDeDadosETabelas();
        }

        [Fact]
        public void Salvar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            PagamentoQueryResult retorno = repositoryPagamento.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.Equal(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.Equal(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.Equal(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.Equal(pagamento.Valor, retorno.Valor);
            Assert.Equal(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Fact]
        public void Atualizar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            pagamento = new Pagamento(
                1,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento - Editado", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            repositoryPagamento.Atualizar(pagamento);

            PagamentoQueryResult retorno = repositoryPagamento.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.Equal(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.Equal(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.Equal(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.Equal(pagamento.Valor, retorno.Valor);
            Assert.Equal(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Fact]
        public void Deletar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Pagamento pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0); 
            repositoryPagamento.Salvar(pagamento1);

            repositoryPagamento.Deletar(1);

            List<PagamentoQueryResult> retorno = repositoryPagamento.Listar();

            Assert.Equal(2, retorno[0].Id);
            Assert.Equal(pagamento1.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.Equal(pagamento1.Empresa.Id, retorno[0].Empresa.Id);
            Assert.Equal(pagamento1.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.Equal(pagamento1.Descricao.ToString(), retorno[0].Descricao);
            Assert.Equal(pagamento1.Valor, retorno[0].Valor);
            Assert.Equal(pagamento1.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);
        }

        [Fact]
        public void Obter()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            PagamentoQueryResult retorno = repositoryPagamento.Obter(1);

            Assert.Equal(1, retorno.Id);
            Assert.Equal(pagamento.TipoPagamento.Id, retorno.TipoPagamento.Id);
            Assert.Equal(pagamento.Empresa.Id, retorno.Empresa.Id);
            Assert.Equal(pagamento.Pessoa.Id, retorno.Pessoa.Id);
            Assert.Equal(pagamento.Descricao.ToString(), retorno.Descricao);
            Assert.Equal(pagamento.Valor, retorno.Valor);
            Assert.Equal(pagamento.DataVencimento.Date, retorno.DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento.DataPagamento).Date, Convert.ToDateTime(retorno.DataPagamento).Date);
        }

        [Fact]
        public void Listar()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Pagamento pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            List<PagamentoQueryResult> retorno = repositoryPagamento.Listar();

            Assert.Equal(1, retorno[0].Id);
            Assert.Equal(pagamento1.TipoPagamento.Id, retorno[1].TipoPagamento.Id);
            Assert.Equal(pagamento0.Empresa.Id, retorno[0].Empresa.Id);
            Assert.Equal(pagamento0.Pessoa.Id, retorno[0].Pessoa.Id);
            Assert.Equal(pagamento0.Descricao.ToString(), retorno[0].Descricao);
            Assert.Equal(pagamento0.Valor, retorno[0].Valor);
            Assert.Equal(pagamento0.DataVencimento.Date, retorno[0].DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento0.DataPagamento).Date, Convert.ToDateTime(retorno[0].DataPagamento).Date);

            Assert.Equal(2, retorno[1].Id);
            Assert.Equal(pagamento0.TipoPagamento.Id, retorno[0].TipoPagamento.Id);
            Assert.Equal(pagamento1.Empresa.Id, retorno[1].Empresa.Id);
            Assert.Equal(pagamento1.Pessoa.Id, retorno[1].Pessoa.Id);
            Assert.Equal(pagamento1.Descricao.ToString(), retorno[1].Descricao);
            Assert.Equal(pagamento1.Valor, retorno[1].Valor);
            Assert.Equal(pagamento1.DataVencimento.Date, retorno[1].DataVencimento.Date);
            Assert.Equal(Convert.ToDateTime(pagamento1.DataPagamento).Date, Convert.ToDateTime(retorno[1].DataPagamento).Date);
        }

        [Fact]
        public void CheckId()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento);

            bool idExistente = repositoryPagamento.CheckId(1);
            bool idNaoExiste = repositoryPagamento.CheckId(25);

            Assert.True(idExistente);
            Assert.False(idNaoExiste);
        }

        [Fact]
        public void LocalizarMaxId()
        {
            TipoPagamento tipoPagamento = new TipoPagamento(0, new Texto("DescriçãoTipoPagamento", "Descrição", 250));
            Empresa empresa = new Empresa(0, new Texto("NomeEmpresa", "Nome", 100), "Logo");
            Pessoa pessoa = new Pessoa(0, new Texto("NomePessoa", "Nome", 100), "ImagemPerfil");
            Pagamento pagamento0 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 0", "Descrição", 250),
                100,
                DateTime.Now.AddDays(1),
                DateTime.Now
            );

            Pagamento pagamento1 = new Pagamento(
                0,
                new TipoPagamento(1),
                new Empresa(1),
                new Pessoa(1),
                new Texto("DescriçãoPagamento 1", "Descrição", 250),
                500,
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(2)
            );

            Mock<IOptions<SettingsInfraData>> mockOptions = new Mock<IOptions<SettingsInfraData>>();
            mockOptions.SetupGet(m => m.Value).Returns(_settingsInfraData);

            TipoPagamentoRepositorio repositoryTipoPagamento = new TipoPagamentoRepositorio(mockOptions.Object);
            repositoryTipoPagamento.Salvar(tipoPagamento);

            EmpresaRepositorio repositoryEmpresa = new EmpresaRepositorio(mockOptions.Object);
            repositoryEmpresa.Salvar(empresa);

            PessoaRepositorio repositoryPessoa = new PessoaRepositorio(mockOptions.Object);
            repositoryPessoa.Salvar(pessoa);

            PagamentoRepositorio repositoryPagamento = new PagamentoRepositorio(mockOptions.Object);
            repositoryPagamento.Salvar(pagamento0);
            repositoryPagamento.Salvar(pagamento1);

            int maxId = repositoryPagamento.LocalizarMaxId();

            Assert.Equal(2, maxId);
        }
    }
}