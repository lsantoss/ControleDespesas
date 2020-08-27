using ControleDespesas.Api.Services;
using ControleDespesas.Api.Settings;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Infra.Data.Repositorio;
using ControleDespesas.Infra.Data.Settings;
using ControleDespesas.Test.AppConfigurations.Factory;
using ControleDespesas.Test.AppConfigurations.Settings;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ControleDespesas.Test.Controllers
{
    public class EmpresaControllerTest : DatabaseFactory
    {
        private HttpClient _client; 
        private string _tokenJWT;

        [SetUp]
        public void Setup() 
        {
            CriarBaseDeDadosETabelas();
            _tokenJWT = ObterTokenJWT();
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ChaveAPI", SettingsTest.ChaveAPI);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenJWT);
        }

        [Test]
        public async Task EmpresaHealthCheckAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("https://localhost:44323/Empresa/v1/HealthCheck");
            
            var responseJson = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<ApiResponseModel<string, Notificacao>>(responseJson);

            Assert.True(responseObj.Sucesso);
            Assert.AreEqual("Sucesso", responseObj.Mensagem);
            Assert.AreEqual("API Controle de Despesas - Empresa OK", responseObj.Dados);
            Assert.Null(responseObj.Erros);
        }

        [TearDown]
        public void TearDown() 
        {
            DroparBaseDeDados();
            _tokenJWT = string.Empty;
            _client = null;
        }

        private string ObterTokenJWT()
        {
            Usuario usuario = new Usuario(0, new Texto("NomeUsuario", "Nome", 50), new SenhaMedia("Senha123"), EPrivilegioUsuario.Admin);
            
            Mock<IOptions<SettingsAPI>> mockOptionsAPI = new Mock<IOptions<SettingsAPI>>();
            mockOptionsAPI.SetupGet(m => m.Value).Returns(_settingsAPI);

            Mock<IOptions<SettingsInfraData>> mockOptionsInfra = new Mock<IOptions<SettingsInfraData>>();
            mockOptionsInfra.SetupGet(m => m.Value).Returns(_settingsInfraData);

            UsuarioRepositorio repository = new UsuarioRepositorio(mockOptionsInfra.Object);
            repository.Salvar(usuario);

            UsuarioQueryResult usuarioQR = repository.Logar(usuario.Login.ToString(), usuario.Senha.ToString());

            return new TokenJWTService(mockOptionsAPI.Object).GenerateToken(usuarioQR);
        }
    }
}
