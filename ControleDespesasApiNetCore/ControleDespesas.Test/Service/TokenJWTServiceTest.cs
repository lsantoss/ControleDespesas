using ControleDespesas.Api.Services;
using ControleDespesas.Test.AppConfigurations.Base;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ControleDespesas.Test.Service
{
    public class TokenJWTServiceTest : BaseTest
    {
        private readonly TokenJWTService _tokenJWTService;

        public TokenJWTServiceTest()
        {
            var optionsAPI = Options.Create(MockSettingsAPI);
            _tokenJWTService = new TokenJWTService(optionsAPI);
        }

        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerateToken()
        {
            var usuarioQR = MockSettingsTest.UsuarioQR;

            var tokenJWT = _tokenJWTService.GenerateToken(usuarioQR);

            TestContext.WriteLine(tokenJWT);

            Assert.IsNotNull(tokenJWT);
            Assert.IsNotEmpty(tokenJWT);
        }

        [TearDown]
        public void TearDown() { }
    }
}