using ControleDespesas.Api.Services;
using ControleDespesas.Domain.Interfaces.Services;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Service
{
    public class TokenJWTServiceTest : BaseTest
    {
        private readonly ITokenJWTService _tokenJWTService;

        public TokenJWTServiceTest()
        {
            _tokenJWTService = new TokenJWTService(MockSettingsAPI);
        }

        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerateToken()
        {
            var usuarioQR = new SettingsTest().UsuarioQR;

            var tokenJWT = _tokenJWTService.GenerateToken(usuarioQR);

            TestContext.WriteLine(tokenJWT);

            Assert.IsNotNull(tokenJWT);
            Assert.IsNotEmpty(tokenJWT);
        }

        [TearDown]
        public void TearDown() { }
    }
}