using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Domain.Pagamentos.Commands.Input;
using ControleDespesas.Domain.Pagamentos.Entities;
using ControleDespesas.Domain.Pagamentos.Enums;
using ControleDespesas.Domain.Pagamentos.Query.Parameters;
using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Pessoas.Query.Parameters;
using ControleDespesas.Domain.TiposPagamentos.Commands.Input;
using ControleDespesas.Domain.TiposPagamentos.Entities;
using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Enums;
using ControleDespesas.Domain.Usuarios.Query.Results;
using ControleDespesas.Infra.Logs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ControleDespesas.Test.AppConfigurations.Settings
{
    public class SettingsTest
    {
        #region [IConfiguration - appsettings.json]

        private IConfiguration _configuration { get; }

        #endregion [IConfiguration - appsettings.json]

        #region [Dados de teste para API]

        public string ControleDespesasAPINetCore { get; }
        public string ChaveAPI { get; }
        public string ChaveJWT { get; }

        #endregion [Dados de teste para API]

        #region [Dados de teste para Banco de Dados]

        public string ConnectionSQLServerReal { get; }
        public string ConnectionSQLServerTest { get; }

        #endregion [Dados de teste para Banco de Dados]

        #region [Dados de teste para Empresa]

        public AdicionarEmpresaCommand EmpresaAdicionarCommand { get; }
        public AtualizarEmpresaCommand EmpresaAtualizarCommand { get; }
        public Empresa Empresa1 { get; }
        public Empresa Empresa2 { get; }
        public Empresa Empresa3 { get; }
        public Empresa Empresa1Editada { get; }

        #endregion [Dados de teste para Empresa]

        #region [Dados de teste para Pessoa]

        public AdicionarPessoaCommand PessoaAdicionarCommand { get; }
        public AtualizarPessoaCommand PessoaAtualizarCommand { get; }
        public ObterPessoasQuery PessoaObterQuery { get; }
        public Pessoa Pessoa1 { get; }
        public Pessoa Pessoa2 { get; }
        public Pessoa Pessoa3 { get; }
        public Pessoa Pessoa1Editada { get; }

        #endregion [Dados de teste para Pessoa]

        #region [Dados de teste para TipoPagamento]

        public AdicionarTipoPagamentoCommand TipoPagamentoAdicionarCommand { get; }
        public AtualizarTipoPagamentoCommand TipoPagamentoAtualizarCommand { get; }
        public TipoPagamento TipoPagamento1 { get; }
        public TipoPagamento TipoPagamento2 { get; }
        public TipoPagamento TipoPagamento3 { get; }
        public TipoPagamento TipoPagamento1Editado { get; }

        #endregion [Dados de teste para TipoPagamento]

        #region [Dados de teste para Pagamento]

        public AdicionarPagamentoCommand PagamentoAdicionarCommand { get; }
        public AtualizarPagamentoCommand PagamentoAtualizarCommand { get; }
        public PagamentoQuery PagamentoQuery { get; }
        public PagamentoQuery PagamentoQueryPagos { get; }
        public PagamentoQuery PagamentoQueryPendentes { get; }
        public PagamentoGastosQuery PagamentoGastosQuery { get; }
        public Pagamento Pagamento1 { get; }
        public Pagamento Pagamento2 { get; }
        public Pagamento Pagamento3 { get; }
        public Pagamento Pagamento1Editado { get; }

        #endregion [Dados de teste para Pagamento]

        #region [Dados de teste para Usuario]

        public AdicionarUsuarioCommand UsuarioAdicionarCommand { get; }
        public AtualizarUsuarioCommand UsuarioAtualizarCommand { get; }
        public LoginUsuarioCommand UsuarioLoginCommand { get; }
        public UsuarioQueryResult UsuarioQR { get; }
        public Usuario Usuario1 { get; }
        public Usuario Usuario2 { get; }
        public Usuario Usuario3 { get; }
        public Usuario Usuario1Editado { get; }

        #endregion [Dados de teste para Usuario]

        #region [Dados de teste para LogRequestResponse]

        public LogRequestResponse LogRequestResponse1 { get; }
        public LogRequestResponse LogRequestResponse2 { get; }

        #endregion [Dados de teste para LogRequestResponse]

        public SettingsTest()
        {
            #region [Setando IConfiguration - appsettings.json]

            _configuration = new ServiceCollection().AddTransient<IConfiguration>(sp => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).BuildServiceProvider().GetService<IConfiguration>();

            #endregion [Setando IConfiguration - appsettings.json]

            #region [Dados de teste para API]

            ControleDespesasAPINetCore = _configuration.GetValue<string>("SettingsTest:ControleDespesasAPINetCore");
            ChaveAPI = _configuration.GetValue<string>("SettingsTest:ChaveAPI");
            ChaveJWT = _configuration.GetValue<string>("SettingsTest:ChaveJWT");

            #endregion [Dados de teste para API]

            #region [Setando dados teste para Banco de Dados]

            ConnectionSQLServerReal = _configuration.GetValue<string>("SettingsTest:ConnectionSQLServerReal");
            ConnectionSQLServerTest = _configuration.GetValue<string>("SettingsTest:ConnectionSQLServerTest");

            #endregion [Setando dados teste para Banco de Dados]

            #region [Setando dados de teste para Empresa]

            EmpresaAdicionarCommand = new AdicionarEmpresaCommand()
            {
                Nome = _configuration.GetValue<string>("SettingsTest:EmpresaAdicionarCommand:Nome"),
                Logo = _configuration.GetValue<string>("SettingsTest:EmpresaAdicionarCommand:Logo")
            };

            EmpresaAtualizarCommand = new AtualizarEmpresaCommand()
            {
                Id = _configuration.GetValue<long>("SettingsTest:EmpresaAtualizarCommand:Id"),
                Nome = _configuration.GetValue<string>("SettingsTest:EmpresaAtualizarCommand:Nome"),
                Logo = _configuration.GetValue<string>("SettingsTest:EmpresaAtualizarCommand:Logo")
            };

            Empresa1 = new Empresa(
                _configuration.GetValue<long>("SettingsTest:Empresa1:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa1:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa1:Logo")
            );

            Empresa2 = new Empresa(
                _configuration.GetValue<long>("SettingsTest:Empresa2:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa2:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa2:Logo")
            );

            Empresa3 = new Empresa(
                _configuration.GetValue<long>("SettingsTest:Empresa3:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa3:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa3:Logo")
            );

            Empresa1Editada = new Empresa(
                _configuration.GetValue<long>("SettingsTest:Empresa1Editada:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa1Editada:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa1Editada:Logo")
            );

            #endregion [Setando dados de teste para Empresa]

            #region [Setando dados de teste para Pessoa]

            PessoaAdicionarCommand = new AdicionarPessoaCommand()
            {
                IdUsuario = _configuration.GetValue<int>("SettingsTest:PessoaAdicionarCommand:IdUsuario"),
                Nome = _configuration.GetValue<string>("SettingsTest:PessoaAdicionarCommand:Nome"),
                ImagemPerfil = _configuration.GetValue<string>("SettingsTest:PessoaAdicionarCommand:ImagemPerfil")
            };

            PessoaAtualizarCommand = new AtualizarPessoaCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PessoaAtualizarCommand:Id"),
                IdUsuario = _configuration.GetValue<int>("SettingsTest:PessoaAtualizarCommand:IdUsuario"),
                Nome = _configuration.GetValue<string>("SettingsTest:PessoaAtualizarCommand:Nome"),
                ImagemPerfil = _configuration.GetValue<string>("SettingsTest:PessoaAtualizarCommand:ImagemPerfil")
            };

            PessoaObterQuery = new ObterPessoasQuery()
            {
                IdUsuario = _configuration.GetValue<int>("SettingsTest:PessoaObterQuery:IdUsuario")
            };

            Pessoa1 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa1:Id"),
                new Usuario(_configuration.GetValue<long>("SettingsTest:Pessoa1:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa1:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa1:ImagemPerfil")
            );

            Pessoa2 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa2:Id"),
                new Usuario(_configuration.GetValue<long>("SettingsTest:Pessoa2:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa2:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa2:ImagemPerfil")
            );

            Pessoa3 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa3:Id"),
                new Usuario(_configuration.GetValue<long>("SettingsTest:Pessoa3:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa3:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa3:ImagemPerfil")
            );

            Pessoa1Editada = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa1Editada:Id"),
                new Usuario(_configuration.GetValue<long>("SettingsTest:Pessoa1Editada:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa1Editada:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa1Editada:ImagemPerfil")
            );

            #endregion [Setando dados de teste para Pessoa]

            #region [Setando dados de teste para TipoPagamento]

            TipoPagamentoAdicionarCommand = new AdicionarTipoPagamentoCommand()
            {
                Descricao = _configuration.GetValue<string>("SettingsTest:TipoPagamentoAdicionarCommand:Descricao")
            };

            TipoPagamentoAtualizarCommand = new AtualizarTipoPagamentoCommand()
            {
                Id = _configuration.GetValue<long>("SettingsTest:TipoPagamentoAtualizarCommand:Id"),
                Descricao = _configuration.GetValue<string>("SettingsTest:TipoPagamentoAtualizarCommand:Descricao")
            };

            TipoPagamento1 = new TipoPagamento(
                _configuration.GetValue<long>("SettingsTest:TipoPagamento1:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento1:Descricao")
            );

            TipoPagamento2 = new TipoPagamento(
                _configuration.GetValue<long>("SettingsTest:TipoPagamento2:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento2:Descricao")
            );

            TipoPagamento3 = new TipoPagamento(
                _configuration.GetValue<long>("SettingsTest:TipoPagamento3:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento3:Descricao")
            );

            TipoPagamento1Editado = new TipoPagamento(
                _configuration.GetValue<long>("SettingsTest:TipoPagamento1Editado:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento1Editado:Descricao")
            );

            #endregion [Setando dados de teste para TipoPagamento]

            #region [Setando dados de teste para Pagamento]

            PagamentoAdicionarCommand = new AdicionarPagamentoCommand()
            {
                IdTipoPagamento = _configuration.GetValue<int>("SettingsTest:PagamentoAdicionarCommand:IdTipoPagamento"),
                IdEmpresa = _configuration.GetValue<int>("SettingsTest:PagamentoAdicionarCommand:IdEmpresa"),
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoAdicionarCommand:IdPessoa"),
                Descricao = _configuration.GetValue<string>("SettingsTest:PagamentoAdicionarCommand:Descricao"),
                Valor = _configuration.GetValue<double>("SettingsTest:PagamentoAdicionarCommand:Valor"),
                DataVencimento = _configuration.GetValue<DateTime>("SettingsTest:PagamentoAdicionarCommand:DataVencimento"),
                DataPagamento = _configuration.GetValue<DateTime?>("SettingsTest:PagamentoAdicionarCommand:DataPagamento"),
                ArquivoPagamento = _configuration.GetValue<string>("SettingsTest:PagamentoAdicionarCommand:ArquivoPagamento"),
                ArquivoComprovante = _configuration.GetValue<string>("SettingsTest:PagamentoAdicionarCommand:ArquivoComprovante")
            };

            PagamentoAtualizarCommand = new AtualizarPagamentoCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PagamentoAtualizarCommand:Id"),
                IdTipoPagamento = _configuration.GetValue<int>("SettingsTest:PagamentoAtualizarCommand:IdTipoPagamento"),
                IdEmpresa = _configuration.GetValue<int>("SettingsTest:PagamentoAtualizarCommand:IdEmpresa"),
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoAtualizarCommand:IdPessoa"),
                Descricao = _configuration.GetValue<string>("SettingsTest:PagamentoAtualizarCommand:Descricao"),
                Valor = _configuration.GetValue<double>("SettingsTest:PagamentoAtualizarCommand:Valor"),
                DataVencimento = _configuration.GetValue<DateTime>("SettingsTest:PagamentoAtualizarCommand:DataVencimento"),
                DataPagamento = _configuration.GetValue<DateTime?>("SettingsTest:PagamentoAtualizarCommand:DataPagamento"),
                ArquivoPagamento = _configuration.GetValue<string>("SettingsTest:PagamentoAtualizarCommand:ArquivoPagamento"),
                ArquivoComprovante = _configuration.GetValue<string>("SettingsTest:PagamentoAtualizarCommand:ArquivoComprovante")
            };

            PagamentoQuery = new PagamentoQuery()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoQuery:IdPessoa"),
                Status = _configuration.GetValue<EPagamentoStatus?>("SettingsTest:PagamentoQuery:Status")
            };

            PagamentoQueryPagos = new PagamentoQuery()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoQueryPagos:IdPessoa"),
                Status = _configuration.GetValue<EPagamentoStatus?>("SettingsTest:PagamentoQueryPagos:Status")
            };

            PagamentoQueryPendentes = new PagamentoQuery()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoQueryPendentes:IdPessoa"),
                Status = _configuration.GetValue<EPagamentoStatus?>("SettingsTest:PagamentoQueryPendentes:Status")
            };

            PagamentoGastosQuery = new PagamentoGastosQuery()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoGastosQuery:IdPessoa"),
                Ano = _configuration.GetValue<int>("SettingsTest:PagamentoGastosQuery:Ano"),
                Mes = _configuration.GetValue<int>("SettingsTest:PagamentoGastosQuery:Mes")
            };

            Pagamento1 = new Pagamento(
                _configuration.GetValue<int>("SettingsTest:Pagamento1:Id"),
                new TipoPagamento(
                    _configuration.GetValue<long>("SettingsTest:Pagamento1:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<long>("SettingsTest:Pagamento1:Empresa:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:Empresa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:Empresa:Logo")
                ),
                new Pessoa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento1:Pessoa:Id"),
                    new Usuario(_configuration.GetValue<int>("SettingsTest:Pagamento1:Pessoa:IdUsuario")),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:Pessoa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:Pessoa:ImagemPerfil")
                ),
                _configuration.GetValue<string>("SettingsTest:Pagamento1:Descricao"),
                _configuration.GetValue<double>("SettingsTest:Pagamento1:Valor"),
                _configuration.GetValue<DateTime>("SettingsTest:Pagamento1:DataVencimento"),
                _configuration.GetValue<DateTime?>("SettingsTest:Pagamento1:DataPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento1:ArquivoPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento1:ArquivoComprovante")
            );

            Pagamento2 = new Pagamento(
                 _configuration.GetValue<int>("SettingsTest:Pagamento2:Id"),
                new TipoPagamento(
                    _configuration.GetValue<long>("SettingsTest:Pagamento2:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<long>("SettingsTest:Pagamento2:Empresa:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:Empresa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:Empresa:Logo")
                ),
                new Pessoa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento2:Pessoa:Id"),
                    new Usuario(_configuration.GetValue<int>("SettingsTest:Pagamento2:Pessoa:IdUsuario")),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:Pessoa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:Pessoa:ImagemPerfil")
                ),
                _configuration.GetValue<string>("SettingsTest:Pagamento2:Descricao"),
                _configuration.GetValue<double>("SettingsTest:Pagamento2:Valor"),
                _configuration.GetValue<DateTime>("SettingsTest:Pagamento2:DataVencimento"),
                _configuration.GetValue<DateTime?>("SettingsTest:Pagamento2:DataPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento2:ArquivoPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento2:ArquivoComprovante")
            );

            Pagamento3 = new Pagamento(
                 _configuration.GetValue<int>("SettingsTest:Pagamento3:Id"),
                new TipoPagamento(
                    _configuration.GetValue<long>("SettingsTest:Pagamento3:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<long>("SettingsTest:Pagamento3:Empresa:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:Empresa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:Empresa:Logo")
                ),
                new Pessoa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento3:Pessoa:Id"),
                    new Usuario(_configuration.GetValue<int>("SettingsTest:Pagamento3:Pessoa:IdUsuario")),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:Pessoa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:Pessoa:ImagemPerfil")
                ),
                _configuration.GetValue<string>("SettingsTest:Pagamento3:Descricao"),
                _configuration.GetValue<double>("SettingsTest:Pagamento3:Valor"),
                _configuration.GetValue<DateTime>("SettingsTest:Pagamento3:DataVencimento"),
                _configuration.GetValue<DateTime?>("SettingsTest:Pagamento3:DataPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento3:ArquivoPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento3:ArquivoComprovante")
            );

            Pagamento1Editado = new Pagamento(
                _configuration.GetValue<int>("SettingsTest:Pagamento1Editado:Id"),
                new TipoPagamento(
                    _configuration.GetValue<long>("SettingsTest:Pagamento1Editado:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<long>("SettingsTest:Pagamento1Editado:Empresa:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:Empresa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:Empresa:Logo")
                ),
                new Pessoa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento1Editado:Pessoa:Id"),
                    new Usuario(_configuration.GetValue<int>("SettingsTest:Pagamento1Editado:Pessoa:IdUsuario")),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:Pessoa:Nome"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:Pessoa:ImagemPerfil")
                ),
                _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:Descricao"),
                _configuration.GetValue<double>("SettingsTest:Pagamento1Editado:Valor"),
                _configuration.GetValue<DateTime>("SettingsTest:Pagamento1Editado:DataVencimento"),
                _configuration.GetValue<DateTime?>("SettingsTest:Pagamento1Editado:DataPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:ArquivoPagamento"),
                _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:ArquivoComprovante")
            );

            #endregion [Setando dados de teste para Pagamento]

            #region[ Setando dados de teste para Usuario]

            UsuarioAdicionarCommand = new AdicionarUsuarioCommand()
            {
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioAdicionarCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioAdicionarCommand:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioAdicionarCommand:Privilegio")
            };

            UsuarioAtualizarCommand = new AtualizarUsuarioCommand()
            {
                Id = _configuration.GetValue<long>("SettingsTest:UsuarioAtualizarCommand:Id"),
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioAtualizarCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioAtualizarCommand:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioAtualizarCommand:Privilegio")
            };

            UsuarioLoginCommand = new LoginUsuarioCommand()
            {
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioLoginCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioLoginCommand:Senha")
            };

            UsuarioQR = new UsuarioQueryResult()
            {
                Id = _configuration.GetValue<long>("SettingsTest:UsuarioQR:Id"),
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioQR:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioQR:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioQR:Privilegio")
            };

            Usuario1 = new Usuario(
                _configuration.GetValue<long>("SettingsTest:Usuario1:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario1:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario1:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario1:Privilegio")
            );

            Usuario2 = new Usuario(
                _configuration.GetValue<long>("SettingsTest:Usuario2:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario2:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario2:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario2:Privilegio")

            );

            Usuario3 = new Usuario(
                _configuration.GetValue<long>("SettingsTest:Usuario3:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario3:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario3:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario3:Privilegio")

            );

            Usuario1Editado = new Usuario(
                _configuration.GetValue<long>("SettingsTest:Usuario1Editado:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario1Editado:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario1Editado:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario1Editado:Privilegio")

            );

            #endregion Setando dados de teste para Usuario]

            #region [Setando dados de teste para LogRequestResponse]

            LogRequestResponse1 = new LogRequestResponse()
            {
                Id = _configuration.GetValue<int>("SettingsTest:LogRequestResponse1:Id"),
                MachineName = _configuration.GetValue<string>("SettingsTest:LogRequestResponse1:MachineName"),
                DataRequest = _configuration.GetValue<DateTime>("SettingsTest:LogRequestResponse1:DataRequest"),
                DataResponse = _configuration.GetValue<DateTime>("SettingsTest:LogRequestResponse1:DataResponse"),
                EndPoint = _configuration.GetValue<string>("SettingsTest:LogRequestResponse1:EndPoint"),
                Request = _configuration.GetValue<string>("SettingsTest:LogRequestResponse1:Request"),
                Response = _configuration.GetValue<string>("SettingsTest:LogRequestResponse1:Response"),
                TempoDuracao = _configuration.GetValue<long?>("SettingsTest:LogRequestResponse1:TempoDuracao")
            };

            LogRequestResponse2 = new LogRequestResponse()
            {
                Id = _configuration.GetValue<int>("SettingsTest:LogRequestResponse2:Id"),
                MachineName = _configuration.GetValue<string>("SettingsTest:LogRequestResponse2:MachineName"),
                DataRequest = _configuration.GetValue<DateTime>("SettingsTest:LogRequestResponse2:DataRequest"),
                DataResponse = _configuration.GetValue<DateTime>("SettingsTest:LogRequestResponse2:DataResponse"),
                EndPoint = _configuration.GetValue<string>("SettingsTest:LogRequestResponse2:EndPoint"),
                Request = _configuration.GetValue<string>("SettingsTest:LogRequestResponse2:Request"),
                Response = _configuration.GetValue<string>("SettingsTest:LogRequestResponse2:Response"),
                TempoDuracao = _configuration.GetValue<long?>("SettingsTest:LogRequestResponse2:TempoDuracao")
            };

            #endregion [Setando dados de teste para LogRequestResponse]
        }
    }
}