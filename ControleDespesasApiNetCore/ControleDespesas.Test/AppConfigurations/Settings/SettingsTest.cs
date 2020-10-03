﻿using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using LSCode.ConexoesBD.Enums;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ControleDespesas.Test.AppConfigurations.Settings
{
    public class SettingsTest
    {
        #region[IConfiguration - appsettings.json]
        private IConfiguration _configuration { get; }
        #endregion

        #region[Dados de teste para API]
        public string ControleDespesasAPINetCore { get; }
        public string ChaveAPI { get; }
        public string ChaveJWT { get; }
        #endregion

        #region[Dados de teste para Banco de Dados]
        public Type TipoBancoDeDdos { get; }
        public EBancoDadosRelacional BancoDeDadosRelacional { get; }
        public EBancoDadosNaoRelacional BancoDeDadosNaoRelacional { get; }

        public string ConnectionSQLServerReal { get; }
        public string ConnectionSQLServerTest { get; }

        public string ConnectionMySqlReal { get; }
        public string ConnectionMySqlTest { get; }

        public string ConnectionSQLiteReal { get; }
        public string ConnectionSQLiteTest { get; }

        public string ConnectionPostgreSQLReal { get; }
        public string ConnectionPostgreSQLTest { get; }

        public string ConnectionOracleReal { get; }
        public string ConnectionOracleTest { get; }

        public string ConnectionMongoDBReal { get; }
        public string ConnectionMongoDBTest { get; }
        #endregion

        #region [Dados de teste para Empresa]
        public AdicionarEmpresaCommand EmpresaAdicionarCommand { get; }
        public AtualizarEmpresaCommand EmpresaAtualizarCommand { get; }
        public ApagarEmpresaCommand EmpresaApagarCommand { get; }
        public ObterEmpresaPorIdCommand EmpresaObterPorIdCommand { get; }
        public Empresa Empresa1 { get; }
        public Empresa Empresa2 { get; }
        public Empresa Empresa3 { get; }
        public Empresa Empresa1Editada { get; }
        #endregion

        #region[Dados de teste para Pessoa]
        public AdicionarPessoaCommand PessoaAdicionarCommand { get; }
        public AtualizarPessoaCommand PessoaAtualizarCommand { get; }
        public ApagarPessoaCommand PessoaApagarCommand { get; }
        public ObterPessoaPorIdCommand PessoaObterPorIdCommand { get; }
        public Pessoa Pessoa1 { get; }
        public Pessoa Pessoa2 { get; }
        public Pessoa Pessoa3 { get; }
        public Pessoa Pessoa1Editada { get; }
        #endregion

        #region[Dados de teste para TipoPagamento]
        public AdicionarTipoPagamentoCommand TipoPagamentoAdicionarCommand { get; }
        public AtualizarTipoPagamentoCommand TipoPagamentoAtualizarCommand { get; }
        public ApagarTipoPagamentoCommand TipoPagamentoApagarCommand { get; }
        public ObterTipoPagamentoPorIdCommand TipoPagamentoObterPorIdCommand { get; }
        public TipoPagamento TipoPagamento1 { get; }
        public TipoPagamento TipoPagamento2 { get; }
        public TipoPagamento TipoPagamento3 { get; }
        public TipoPagamento TipoPagamento1Editado { get; }
        #endregion

        #region[Dados de teste para Pagamento]
        public AdicionarPagamentoCommand PagamentoAdicionarCommand { get; }
        public AtualizarPagamentoCommand PagamentoAtualizarCommand { get; }
        public ApagarPagamentoCommand PagamentoApagarCommand { get; }
        public ObterPagamentoPorIdCommand PagamentoObterPorIdCommand { get; }
        public Pagamento Pagamento1 { get; }
        public Pagamento Pagamento2 { get; }
        public Pagamento Pagamento3 { get; }
        public Pagamento Pagamento1Editado { get; }
        #endregion

        #region[Dados de teste para Usuario]
        public AdicionarUsuarioCommand UsuarioAdicionarCommand { get; }
        public AtualizarUsuarioCommand UsuarioAtualizarCommand { get; }
        public ApagarUsuarioCommand UsuarioApagarCommand { get; }
        public ObterUsuarioPorIdCommand UsuarioObterPorIdCommand { get; }
        public LoginUsuarioCommand UsuarioLoginCommand { get; }
        public Usuario Usuario1 { get; }
        public Usuario Usuario2 { get; }
        public Usuario Usuario3 { get; }
        public Usuario Usuario1Editado { get; }
        #endregion

        public SettingsTest()
        {
            #region[Setando IConfiguration - appsettings.json]
            _configuration = new ServiceCollection().AddTransient<IConfiguration>(sp => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).BuildServiceProvider().GetService<IConfiguration>();
            #endregion

            #region[Dados de teste para API]
            ControleDespesasAPINetCore = _configuration.GetValue<string>["SettingsTest:ControleDespesasAPINetCore"];
            ChaveAPI = _configuration["SettingsTest:ChaveAPI"];
            ChaveJWT = _configuration["SettingsTest:ChaveJWT"];
            #endregion

            #region[Setando dados teste para Banco de Dados]
            TipoBancoDeDdos = typeof(EBancoDadosRelacional);
            BancoDeDadosRelacional = EBancoDadosRelacional.SQLServer;
            BancoDeDadosNaoRelacional = EBancoDadosNaoRelacional.MongoDB;
            ConnectionSQLServerReal = _configuration["SettingsTest:ConnectionSQLServerReal"];
            ConnectionSQLServerTest = _configuration["SettingsTest:ConnectionSQLServerTest"];
            ConnectionMySqlReal = _configuration["SettingsTest:ConnectionMySqlReal"];
            ConnectionMySqlTest = _configuration["SettingsTest:ConnectionMySqlTest"];
            ConnectionSQLiteReal = _configuration["SettingsTest:ConnectionSQLiteReal"];
            ConnectionSQLiteTest = _configuration["SettingsTest:ConnectionSQLiteTest"];
            ConnectionPostgreSQLReal = _configuration["SettingsTest:ConnectionPostgreSQLReal"];
            ConnectionPostgreSQLTest = _configuration["SettingsTest:ConnectionPostgreSQLTest"];
            ConnectionOracleReal = _configuration["SettingsTest:ConnectionOracleReal"];
            ConnectionOracleTest = _configuration["SettingsTest:ConnectionOracleTest"];
            ConnectionMongoDBReal = _configuration["SettingsTest:ConnectionMongoDBReal"];
            ConnectionMongoDBTest = _configuration["SettingsTest:ConnectionMongoDBTest"];
            #endregion

            #region[Setando dados de teste para Empresa]
            EmpresaAdicionarCommand = new AdicionarEmpresaCommand()
            {
                Nome = _configuration["SettingsTest:EmpresaAdicionarCommand:Nome"],
                Logo = _configuration["SettingsTest:EmpresaAdicionarCommand:Logo"]
            };

            EmpresaAtualizarCommand = new AtualizarEmpresaCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:EmpresaAtualizarCommand:Id"]),
                Nome = _configuration["SettingsTest:EmpresaAtualizarCommand:Nome"],
                Logo = _configuration["SettingsTest:EmpresaAtualizarCommand:Logo"]
            };

            EmpresaApagarCommand = new ApagarEmpresaCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:EmpresaApagarCommand:Id"])
            };

            EmpresaObterPorIdCommand = new ObterEmpresaPorIdCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:EmpresaObterPorIdCommand:Id"])
            };

            Empresa1 = new Empresa(
                Convert.ToInt32(_configuration["SettingsTest:Empresa1:Id"]),
                new Texto(_configuration["SettingsTest:Empresa1:Nome"], "Nome", 100),
                _configuration["SettingsTest:Empresa1:Logo"]
            );

            Empresa2 = new Empresa(
                Convert.ToInt32(_configuration["SettingsTest:Empresa2:Id"]),
                new Texto(_configuration["SettingsTest:Empresa2:Nome"], "Nome", 100),
                _configuration["SettingsTest:Empresa2:Logo"]
            );

            Empresa3 = new Empresa(
                Convert.ToInt32(_configuration["SettingsTest:Empresa3:Id"]),
                new Texto(_configuration["SettingsTest:Empresa3:Nome"], "Nome", 100),
                _configuration["SettingsTest:Empresa3:Logo"]
            );

            Empresa1Editada = new Empresa(
                Convert.ToInt32(_configuration["SettingsTest:Empresa1Editada:Id"]),
                new Texto(_configuration["SettingsTest:Empresa1Editada:Nome"], "Nome", 100),
                _configuration["SettingsTest:Empresa1Editada:Logo"]
            );
            #endregion

            #region[Setando dados de teste para Pessoa]
            PessoaAdicionarCommand = new AdicionarPessoaCommand()
            {
                Nome = _configuration["SettingsTest:PessoaAdicionarCommand:Nome"],
                ImagemPerfil = _configuration["SettingsTest:PessoaAdicionarCommand:ImagemPerfil"]
            };

            PessoaAtualizarCommand = new AtualizarPessoaCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PessoaAtualizarCommand:Id"]),
                Nome = _configuration["SettingsTest:PessoaAtualizarCommand:Nome"],
                ImagemPerfil = _configuration["SettingsTest:PessoaAtualizarCommand:ImagemPerfil"]
            };

            PessoaApagarCommand = new ApagarPessoaCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PessoaApagarCommand:Id"])
            };

            PessoaObterPorIdCommand = new ObterPessoaPorIdCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PessoaObterPorIdCommand:Id"])
            };

            Pessoa1 = new Pessoa(
                Convert.ToInt32(_configuration["SettingsTest:Pessoa1:Id"]),
                new Texto(_configuration["SettingsTest:Pessoa1:Nome"], "Nome", 100),
                _configuration["SettingsTest:Pessoa1:ImagemPerfil"]
            );

            Pessoa2 = new Pessoa(
                Convert.ToInt32(_configuration["SettingsTest:Pessoa2:Id"]),
                new Texto(_configuration["SettingsTest:Pessoa2:Nome"], "Nome", 100),
                _configuration["SettingsTest:Pessoa2:ImagemPerfil"]
            );

            Pessoa3 = new Pessoa(
                Convert.ToInt32(_configuration["SettingsTest:Pessoa3:Id"]),
                new Texto(_configuration["SettingsTest:Pessoa3:Nome"], "Nome", 100),
                _configuration["SettingsTest:Pessoa3:ImagemPerfil"]
            );

            Pessoa1Editada = new Pessoa(
                Convert.ToInt32(_configuration["SettingsTest:Pessoa1Editada:Id"]),
                new Texto(_configuration["SettingsTest:Pessoa1Editada:Nome"], "Nome", 100),
                _configuration["SettingsTest:Pessoa1Editada:ImagemPerfil"]
            );
            #endregion

            #region[Setando dados de teste para TipoPagamento]
            TipoPagamentoAdicionarCommand = new AdicionarTipoPagamentoCommand()
            {
                Descricao = _configuration["SettingsTest:TipoPagamentoAdicionarCommand:Descricao"]
            };

            TipoPagamentoAtualizarCommand = new AtualizarTipoPagamentoCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:TipoPagamentoAtualizarCommand:Id"]),
                Descricao = _configuration["SettingsTest:TipoPagamentoAtualizarCommand:Descricao"]
            };

            TipoPagamentoApagarCommand = new ApagarTipoPagamentoCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:TipoPagamentoApagarCommand:Id"])
            };

            TipoPagamentoObterPorIdCommand = new ObterTipoPagamentoPorIdCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:TipoPagamentoObterPorIdCommand:Id"])
            };

            TipoPagamento1 = new TipoPagamento(
                Convert.ToInt32(_configuration["SettingsTest:TipoPagamento1:Id"]),
                new Texto(_configuration["SettingsTest:TipoPagamento1:Descricao"], "Descrição", 250)
            );

            TipoPagamento2 = new TipoPagamento(
                Convert.ToInt32(_configuration["SettingsTest:TipoPagamento2:Id"]),
                new Texto(_configuration["SettingsTest:TipoPagamento2:Descricao"], "Descrição", 250)
            );

            TipoPagamento3 = new TipoPagamento(
                Convert.ToInt32(_configuration["SettingsTest:TipoPagamento3:Id"]),
                new Texto(_configuration["SettingsTest:TipoPagamento3:Descricao"], "Descrição", 250)
            );

            TipoPagamento1Editado = new TipoPagamento(
                Convert.ToInt32(_configuration["SettingsTest:TipoPagamento1Editado:Id"]),
                new Texto(_configuration["SettingsTest:TipoPagamento1Editado:Descricao"], "Descrição", 250)
            );
            #endregion

            #region[Setando dados de teste para Pagamento]
            PagamentoAdicionarCommand = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = Convert.ToInt32(_configuration["SettingsTest:PagamentoAdicionarCommand:IdTipoPagamento"]),
                IdEmpresa = Convert.ToInt32(_configuration["SettingsTest:PagamentoAdicionarCommand:IdEmpresa"]),
                IdPessoa = Convert.ToInt32(_configuration["SettingsTest:PagamentoAdicionarCommand:IdPessoa"]),
                Descricao = _configuration["SettingsTest:PagamentoAdicionarCommand:Descricao"],
                Valor = Convert.ToDouble(_configuration["SettingsTest:PagamentoAdicionarCommand:Valor"]),
                DataVencimento = DateTime.Parse(_configuration["SettingsTest:PagamentoAdicionarCommand:DataVencimento"]),
                DataPagamento = DateTime.Parse(_configuration["SettingsTest:PagamentoAdicionarCommand:DataPagamento"])
            };

            PagamentoAtualizarCommand = new AtualizarPagamentoCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PagamentoAtualizarCommand:Id"]),
                IdTipoPagamento = Convert.ToInt32(_configuration["SettingsTest:PagamentoAtualizarCommand:IdTipoPagamento"]),
                IdEmpresa = Convert.ToInt32(_configuration["SettingsTest:PagamentoAtualizarCommand:IdEmpresa"]),
                IdPessoa = Convert.ToInt32(_configuration["SettingsTest:PagamentoAtualizarCommand:IdPessoa"]),
                Descricao = _configuration["SettingsTest:PagamentoAtualizarCommand:Descricao"],
                Valor = Convert.ToDouble(_configuration["SettingsTest:PagamentoAtualizarCommand:Valor"]),
                DataVencimento = DateTime.Parse(_configuration["SettingsTest:PagamentoAtualizarCommand:DataVencimento"]),
                DataPagamento = DateTime.Parse(_configuration["SettingsTest:PagamentoAtualizarCommand:DataPagamento"])
            };

            PagamentoApagarCommand = new ApagarPagamentoCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PagamentoApagarCommand:Id"])
            };

            PagamentoObterPorIdCommand = new ObterPagamentoPorIdCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:PagamentoObterPorIdCommand:Id"])
            };

            Pagamento1 = new Pagamento(
                Convert.ToInt32(_configuration["SettingsTest:Pagamento1:Id"]),
                new TipoPagamento(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1:TipoPagamento:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1:TipoPagamento:Descricao"], "Descrição", 250)
                ),
                new Empresa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1:Empresa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1:Empresa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento1:Empresa:Logo"]
                ),
                new Pessoa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1:Pessoa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1:Pessoa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento1:Pessoa:ImagemPerfil"]
                ),
                new Texto(_configuration["SettingsTest:Pagamento1:Descricao"], "Descrição", 250),
                Convert.ToDouble(_configuration["SettingsTest:Pagamento1:Valor"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento1:DataVencimento"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento1:DataPagamento"])
            );

            Pagamento2 = new Pagamento(
                Convert.ToInt32(_configuration["SettingsTest:Pagamento2:Id"]),
                new TipoPagamento(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento2:TipoPagamento:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento2:TipoPagamento:Descricao"], "Descrição", 250)
                ),
                new Empresa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento2:Empresa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento2:Empresa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento2:Empresa:Logo"]
                ),
                new Pessoa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento2:Pessoa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento2:Pessoa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento2:Pessoa:ImagemPerfil"]
                ),
                new Texto(_configuration["SettingsTest:Pagamento2:Descricao"], "Descrição", 250),
                Convert.ToDouble(_configuration["SettingsTest:Pagamento2:Valor"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento2:DataVencimento"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento2:DataPagamento"])
            );

            Pagamento3 = new Pagamento(
                Convert.ToInt32(_configuration["SettingsTest:Pagamento3:Id"]),
                new TipoPagamento(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento3:TipoPagamento:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento3:TipoPagamento:Descricao"], "Descrição", 250)
                ),
                new Empresa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento3:Empresa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento3:Empresa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento3:Empresa:Logo"]
                ),
                new Pessoa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento3:Pessoa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento3:Pessoa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento3:Pessoa:ImagemPerfil"]
                ),
                new Texto(_configuration["SettingsTest:Pagamento3:Descricao"], "Descrição", 250),
                Convert.ToDouble(_configuration["SettingsTest:Pagamento3:Valor"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento3:DataVencimento"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento3:DataPagamento"])
            );

            Pagamento1Editado = new Pagamento(
                Convert.ToInt32(_configuration["SettingsTest:Pagamento1Editada:Id"]),
                new TipoPagamento(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1Editada:TipoPagamento:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1Editada:TipoPagamento:Descricao"], "Descrição", 250)
                ),
                new Empresa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1Editada:Empresa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1Editada:Empresa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento1Editada:Empresa:Logo"]
                ),
                new Pessoa(
                    Convert.ToInt32(_configuration["SettingsTest:Pagamento1Editada:Pessoa:Id"]),
                    new Texto(_configuration["SettingsTest:Pagamento1Editada:Pessoa:Nome"], "Nome", 100),
                    _configuration["SettingsTest:Pagamento1Editada:Pessoa:ImagemPerfil"]
                ),
                new Texto(_configuration["SettingsTest:Pagamento1Editada:Descricao"], "Descrição", 250),
                Convert.ToDouble(_configuration["SettingsTest:Pagamento1Editada:Valor"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento1Editada:DataVencimento"]),
                DateTime.Parse(_configuration["SettingsTest:Pagamento1Editada:DataPagamento"])
            );
            #endregion

            #region[Setando dados de teste para Usuario]
            UsuarioAdicionarCommand = new AdicionarUsuarioCommand()
            {
                Login = _configuration["SettingsTest:UsuarioAdicionarCommand:Login"],
                Senha = _configuration["SettingsTest:UsuarioAdicionarCommand:Senha"],
                Privilegio = (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:UsuarioAdicionarCommand:Privilegio"])
            };

            UsuarioAtualizarCommand = new AtualizarUsuarioCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:UsuarioAtualizarCommand:Id"]),
                Login = _configuration["SettingsTest:UsuarioAtualizarCommand:Login"],
                Senha = _configuration["SettingsTest:UsuarioAtualizarCommand:Senha"],
                Privilegio = (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:UsuarioAtualizarCommand:Privilegio"])
            };

            UsuarioApagarCommand = new ApagarUsuarioCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:UsuarioApagarCommand:Id"])
            };

            UsuarioObterPorIdCommand = new ObterUsuarioPorIdCommand()
            {
                Id = Convert.ToInt32(_configuration["SettingsTest:UsuarioObterPorIdCommand:Id"])
            };

            UsuarioLoginCommand = new LoginUsuarioCommand()
            {
                Login = _configuration["SettingsTest:UsuarioLoginCommand:Login"],
                Senha = _configuration["SettingsTest:UsuarioLoginCommand:Senha"]
            };

            Usuario1 = new Usuario(
                Convert.ToInt32(_configuration["SettingsTest:Usuario1:Id"]),
                new Texto(_configuration["SettingsTest:Usuario1:Login"], "Login", 50),
                new SenhaMedia(_configuration["SettingsTest:Usuario1:Senha"]),
                (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:Usuario1:Privilegio"])

            );

            Usuario2 = new Usuario(
                Convert.ToInt32(_configuration["SettingsTest:Usuario2:Id"]),
                new Texto(_configuration["SettingsTest:Usuario2:Login"], "Login", 50),
                new SenhaMedia(_configuration["SettingsTest:Usuario2:Senha"]),
                (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:Usuario2:Privilegio"])

            );

            Usuario3 = new Usuario(
                Convert.ToInt32(_configuration["SettingsTest:Usuario3:Id"]),
                new Texto(_configuration["SettingsTest:Usuario3:Login"], "Login", 50),
                new SenhaMedia(_configuration["SettingsTest:Usuario3:Senha"]),
                (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:Usuario3:Privilegio"])

            );

            Usuario1Editado = new Usuario(
                Convert.ToInt32(_configuration["SettingsTest:Usuario1Editado:Id"]),
                new Texto(_configuration["SettingsTest:Usuario1Editado:Login"], "Login", 50),
                new SenhaMedia(_configuration["SettingsTest:Usuario1Editado:Senha"]),
                (EPrivilegioUsuario)Convert.ToInt32(_configuration["SettingsTest:Usuario1Editado:Privilegio"])

            );
            #endregion
        }        
    }
}