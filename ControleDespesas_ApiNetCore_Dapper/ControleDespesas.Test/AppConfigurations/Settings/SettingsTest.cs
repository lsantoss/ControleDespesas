using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Pagamento.Input;
using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Enums;
using ControleDespesas.Domain.Query.Usuario;
using LSCode.ConexoesBD.Enums;
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
        public ObterPessoasPorIdUsuarioCommand PessoaObterPorIdUsuarioCommand { get; }
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
        public ObterPagamentoPorIdPessoaCommand PagamentoObterPorIdPessoaCommand { get; }
        public ObterArquivoPagamentoCommand PagamentoObterArquivoPagamentoCommand { get; set; }
        public ObterArquivoComprovanteCommand PagamentoObterArquivoComprovanteCommand { get; set; }
        public ObterGastosCommand PagamentoObterGastosCommand { get; }
        public ObterGastosAnoCommand PagamentoObterGastosAnoCommand { get; }
        public ObterGastosAnoMesCommand PagamentoObterGastosAnoMesCommand { get; }
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
        public UsuarioQueryResult UsuarioQR { get; }
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
            ControleDespesasAPINetCore = _configuration.GetValue<string>("SettingsTest:ControleDespesasAPINetCore");
            ChaveAPI = _configuration.GetValue<string>("SettingsTest:ChaveAPI");
            ChaveJWT = _configuration.GetValue<string>("SettingsTest:ChaveJWT");
            #endregion

            #region[Setando dados teste para Banco de Dados]
            TipoBancoDeDdos = _configuration.GetValue<int>("SettingsTest:TipoBancoDeDdos") == 1 ? typeof(EBancoDadosRelacional) : typeof(EBancoDadosNaoRelacional);
            BancoDeDadosRelacional = _configuration.GetValue<EBancoDadosRelacional>("SettingsTest:BancoDeDadosRelacional");
            BancoDeDadosNaoRelacional = _configuration.GetValue<EBancoDadosNaoRelacional>("SettingsTest:BancoDeDadosNaoRelacional");
            ConnectionSQLServerReal = _configuration.GetValue<string>("SettingsTest:ConnectionSQLServerReal");
            ConnectionSQLServerTest = _configuration.GetValue<string>("SettingsTest:ConnectionSQLServerTest");
            ConnectionMySqlReal = _configuration.GetValue<string>("SettingsTest:ConnectionMySqlReal");
            ConnectionMySqlTest = _configuration.GetValue<string>("SettingsTest:ConnectionMySqlTest");
            ConnectionSQLiteReal = _configuration.GetValue<string>("SettingsTest:ConnectionSQLiteReal");
            ConnectionSQLiteTest = _configuration.GetValue<string>("SettingsTest:ConnectionSQLiteTest");
            ConnectionPostgreSQLReal = _configuration.GetValue<string>("SettingsTest:ConnectionPostgreSQLReal");
            ConnectionPostgreSQLTest = _configuration.GetValue<string>("SettingsTest:ConnectionPostgreSQLTest");
            ConnectionOracleReal = _configuration.GetValue<string>("SettingsTest:ConnectionOracleReal");
            ConnectionOracleTest = _configuration.GetValue<string>("SettingsTest:ConnectionOracleTest");
            ConnectionMongoDBReal = _configuration.GetValue<string>("SettingsTest:ConnectionMongoDBReal");
            ConnectionMongoDBTest = _configuration.GetValue<string>("SettingsTest:ConnectionMongoDBTest");
            #endregion

            #region[Setando dados de teste para Empresa]
            EmpresaAdicionarCommand = new AdicionarEmpresaCommand()
            {
                Nome = _configuration.GetValue<string>("SettingsTest:EmpresaAdicionarCommand:Nome"),
                Logo = _configuration.GetValue<string>("SettingsTest:EmpresaAdicionarCommand:Logo")
            };

            EmpresaAtualizarCommand = new AtualizarEmpresaCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:EmpresaAtualizarCommand:Id"),
                Nome = _configuration.GetValue<string>("SettingsTest:EmpresaAtualizarCommand:Nome"),
                Logo = _configuration.GetValue<string>("SettingsTest:EmpresaAtualizarCommand:Logo")
            };

            EmpresaApagarCommand = new ApagarEmpresaCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:EmpresaApagarCommand:Id")
            };

            EmpresaObterPorIdCommand = new ObterEmpresaPorIdCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:EmpresaObterPorIdCommand:Id")
            };

            Empresa1 = new Empresa(
                _configuration.GetValue<int>("SettingsTest:Empresa1:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa1:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa1:Logo")
            );

            Empresa2 = new Empresa(
                _configuration.GetValue<int>("SettingsTest:Empresa2:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa2:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa2:Logo")
            );

            Empresa3 = new Empresa(
                _configuration.GetValue<int>("SettingsTest:Empresa3:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa3:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa3:Logo")
            );

            Empresa1Editada = new Empresa(
                _configuration.GetValue<int>("SettingsTest:Empresa1Editada:Id"),
                _configuration.GetValue<string>("SettingsTest:Empresa1Editada:Nome"),
                _configuration.GetValue<string>("SettingsTest:Empresa1Editada:Logo")
            );
            #endregion

            #region[Setando dados de teste para Pessoa]
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

            PessoaApagarCommand = new ApagarPessoaCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PessoaApagarCommand:Id")
            };

            PessoaObterPorIdCommand = new ObterPessoaPorIdCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PessoaObterPorIdCommand:Id")
            };

            PessoaObterPorIdUsuarioCommand = new ObterPessoasPorIdUsuarioCommand()
            {
                IdUsuario = _configuration.GetValue<int>("SettingsTest:PessoaObterPorIdUsuarioCommand:IdUsuario")
            };

            Pessoa1 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa1:Id"),
                new Usuario(_configuration.GetValue<int>("SettingsTest:Pessoa1:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa1:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa1:ImagemPerfil")
            );

            Pessoa2 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa2:Id"),
                new Usuario(_configuration.GetValue<int>("SettingsTest:Pessoa2:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa2:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa2:ImagemPerfil")
            );

            Pessoa3 = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa3:Id"),
                new Usuario(_configuration.GetValue<int>("SettingsTest:Pessoa3:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa3:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa3:ImagemPerfil")
            );

            Pessoa1Editada = new Pessoa(
                _configuration.GetValue<int>("SettingsTest:Pessoa1Editada:Id"),
                new Usuario(_configuration.GetValue<int>("SettingsTest:Pessoa1Editada:IdUsuario")),
                _configuration.GetValue<string>("SettingsTest:Pessoa1Editada:Nome"),
                _configuration.GetValue<string>("SettingsTest:Pessoa1Editada:ImagemPerfil")
            );
            #endregion

            #region[Setando dados de teste para TipoPagamento]
            TipoPagamentoAdicionarCommand = new AdicionarTipoPagamentoCommand()
            {
                Descricao = _configuration.GetValue<string>("SettingsTest:TipoPagamentoAdicionarCommand:Descricao")
            };

            TipoPagamentoAtualizarCommand = new AtualizarTipoPagamentoCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:TipoPagamentoAtualizarCommand:Id"),
                Descricao = _configuration.GetValue<string>("SettingsTest:TipoPagamentoAtualizarCommand:Descricao")
            };

            TipoPagamentoApagarCommand = new ApagarTipoPagamentoCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:TipoPagamentoApagarCommand:Id")
            };

            TipoPagamentoObterPorIdCommand = new ObterTipoPagamentoPorIdCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:TipoPagamentoObterPorIdCommand:Id")
            };

            TipoPagamento1 = new TipoPagamento(
                _configuration.GetValue<int>("SettingsTest:TipoPagamento1:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento1:Descricao")
            );

            TipoPagamento2 = new TipoPagamento(
                _configuration.GetValue<int>("SettingsTest:TipoPagamento2:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento2:Descricao")
            );

            TipoPagamento3 = new TipoPagamento(
                _configuration.GetValue<int>("SettingsTest:TipoPagamento3:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento3:Descricao")
            );

            TipoPagamento1Editado = new TipoPagamento(
                _configuration.GetValue<int>("SettingsTest:TipoPagamento1Editado:Id"),
                _configuration.GetValue<string>("SettingsTest:TipoPagamento1Editado:Descricao")
            );
            #endregion

            #region[Setando dados de teste para Pagamento]
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

            PagamentoApagarCommand = new ApagarPagamentoCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PagamentoApagarCommand:Id")
            };

            PagamentoObterGastosCommand = new ObterGastosCommand()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosCommand:IdPessoa")
            };

            PagamentoObterGastosAnoCommand = new ObterGastosAnoCommand()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosAnoCommand:IdPessoa"),
                Ano = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosAnoCommand:Ano")
            };

            PagamentoObterGastosAnoMesCommand = new ObterGastosAnoMesCommand()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosAnoMesCommand:IdPessoa"),
                Ano = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosAnoMesCommand:Ano"),
                Mes = _configuration.GetValue<int>("SettingsTest:PagamentoObterGastosAnoMesCommand:Mes")
            };

            PagamentoObterPorIdCommand = new ObterPagamentoPorIdCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:PagamentoObterPorIdCommand:Id")
            };

            PagamentoObterPorIdPessoaCommand = new ObterPagamentoPorIdPessoaCommand()
            {
                IdPessoa = _configuration.GetValue<int>("SettingsTest:PagamentoObterPorIdPessoaCommand:IdPessoa")
            };

            PagamentoObterArquivoPagamentoCommand = new ObterArquivoPagamentoCommand()
            {
                IdPagamento = _configuration.GetValue<int>("SettingsTest:PagamentoObterArquivoPagamentoCommand:IdPagamento")
            };

            PagamentoObterArquivoComprovanteCommand = new ObterArquivoComprovanteCommand()
            {
                IdPagamento = _configuration.GetValue<int>("SettingsTest:PagamentoObterArquivoComprovanteCommand:IdPagamento")
            };

            Pagamento1 = new Pagamento(
                _configuration.GetValue<int>("SettingsTest:Pagamento1:Id"),
                new TipoPagamento(
                    _configuration.GetValue<int>("SettingsTest:Pagamento1:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento1:Empresa:Id"),
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
                    _configuration.GetValue<int>("SettingsTest:Pagamento2:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento2:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento2:Empresa:Id"),
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
                    _configuration.GetValue<int>("SettingsTest:Pagamento3:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento3:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento3:Empresa:Id"),
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
                    _configuration.GetValue<int>("SettingsTest:Pagamento1Editado:TipoPagamento:Id"),
                    _configuration.GetValue<string>("SettingsTest:Pagamento1Editado:TipoPagamento:Descricao")
                ),
                new Empresa(
                    _configuration.GetValue<int>("SettingsTest:Pagamento1Editado:Empresa:Id"),
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
            #endregion

            #region[Setando dados de teste para Usuario]
            UsuarioAdicionarCommand = new AdicionarUsuarioCommand()
            {
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioAdicionarCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioAdicionarCommand:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioAdicionarCommand:Privilegio")
            };

            UsuarioAtualizarCommand = new AtualizarUsuarioCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:UsuarioAtualizarCommand:Id"),
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioAtualizarCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioAtualizarCommand:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioAtualizarCommand:Privilegio")
            };

            UsuarioApagarCommand = new ApagarUsuarioCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:UsuarioApagarCommand:Id")
            };

            UsuarioObterPorIdCommand = new ObterUsuarioPorIdCommand()
            {
                Id = _configuration.GetValue<int>("SettingsTest:UsuarioObterPorIdCommand:Id")
            };

            UsuarioLoginCommand = new LoginUsuarioCommand()
            {
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioLoginCommand:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioLoginCommand:Senha")
            };

            UsuarioQR = new UsuarioQueryResult()
            {
                Id = _configuration.GetValue<int>("SettingsTest:UsuarioQR:Id"),
                Login = _configuration.GetValue<string>("SettingsTest:UsuarioQR:Login"),
                Senha = _configuration.GetValue<string>("SettingsTest:UsuarioQR:Senha"),
                Privilegio = _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:UsuarioQR:Privilegio")
            };

            Usuario1 = new Usuario(
                _configuration.GetValue<int>("SettingsTest:Usuario1:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario1:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario1:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario1:Privilegio")
            );

            Usuario2 = new Usuario(
                _configuration.GetValue<int>("SettingsTest:Usuario2:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario2:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario2:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario2:Privilegio")

            );

            Usuario3 = new Usuario(
                _configuration.GetValue<int>("SettingsTest:Usuario3:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario3:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario3:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario3:Privilegio")

            );

            Usuario1Editado = new Usuario(
                _configuration.GetValue<int>("SettingsTest:Usuario1Editado:Id"),
                _configuration.GetValue<string>("SettingsTest:Usuario1Editado:Login"),
                _configuration.GetValue<string>("SettingsTest:Usuario1Editado:Senha"),
                _configuration.GetValue<EPrivilegioUsuario>("SettingsTest:Usuario1Editado:Privilegio")

            );
            #endregion
        }        
    }
}