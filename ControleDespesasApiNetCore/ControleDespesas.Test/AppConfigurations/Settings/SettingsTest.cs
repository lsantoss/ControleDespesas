using ControleDespesas.Dominio.Commands.Empresa.Input;
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
        #endregion

        #region[Dados de teste para TipoPagamento]
        #endregion

        #region[Dados de teste para Pagamento]
        #endregion

        #region[Dados de teste para Usuario]
        public AdicionarUsuarioCommand UsuarioAdicionarCommand { get; set; }
        public AtualizarUsuarioCommand UsuarioAtualizarCommand { get; set; }
        public ApagarUsuarioCommand UsuarioApagarCommand { get; set; }
        public ObterUsuarioPorIdCommand UsuarioObterPorIdCommand { get; set; }
        public LoginUsuarioCommand UsuarioLoginCommand { get; set; }
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
            #endregion

            #region[Setando dados de teste para TipoPagamento]
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
            #endregion
        }        
    }
}