using ControleDespesas.Dominio.Commands.Empresa.Input;
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
        public TipoPagamento TipoPagamento1Editada { get; }
        #endregion

        #region[Dados de teste para Pagamento]
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

        #region[Dados de teste para utlilizar Banco de Dados]
        public static string ControleDespesasAPINetCore { get; } = @"https://localhost:44323/";
        public static string ChaveAPI { get; } = @"3150112e-5285-43a8-bc71-b100fe7233d5";
        public static string ChaveJWT { get; } = @"(B3]U5N{ho+KdXxB%>Q,Fblh9E;O:NE*O8!ke?b@eM,7kk8Ph{DnRp+}u!Hs?LbE+CpP[*}X^&^fR4w0u0E$H{621%Mr[nw#qC}";

        public static Type TipoBancoDeDdos { get; } = typeof(EBancoDadosRelacional);
        public static EBancoDadosRelacional BancoDeDadosRelacional { get; } = EBancoDadosRelacional.SQLServer;
        public static EBancoDadosNaoRelacional BancoDeDadosNaoRelacional { get; } = EBancoDadosNaoRelacional.MongoDB;

        public static string ConnectionSQLServerReal { get; } = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesas;Data Source=SANTOS-PC\SQLEXPRESS;";
        public static string ConnectionSQLServerTest { get; } = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesasTest;Data Source=SANTOS-PC\SQLEXPRESS;";

        public static string ConnectionMySqlReal { get; } = @"";
        public static string ConnectionMySqlTest { get; } = @"";

        public static string ConnectionSQLiteReal { get; } = @"";
        public static string ConnectionSQLiteTest { get; } = @"";

        public static string ConnectionPostgreSQLReal { get; } = @"";
        public static string ConnectionPostgreSQLTest { get; } = @"";

        public static string ConnectionOracleReal { get; } = @"";
        public static string ConnectionOracleTest { get; } = @"";

        public static string ConnectionMongoDBReal { get; } = @"";
        public static string ConnectionMongoDBTest { get; } = @"";
        #endregion

        public SettingsTest()
        {
            #region[Setando IConfiguration - appsettings.json]
            _configuration = new ServiceCollection().AddTransient<IConfiguration>(sp => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).BuildServiceProvider().GetService<IConfiguration>();
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

            TipoPagamento1Editada = new TipoPagamento(
                Convert.ToInt32(_configuration["SettingsTest:TipoPagamento1Editada:Id"]),
                new Texto(_configuration["SettingsTest:TipoPagamento1Editada:Descricao"], "Descrição", 250)
            );
            #endregion

            #region[Setando dados de teste para Pagamento]
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