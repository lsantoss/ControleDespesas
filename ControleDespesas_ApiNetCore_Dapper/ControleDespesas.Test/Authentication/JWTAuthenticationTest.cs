using ControleDespesas.Api.Authentication;
using ControleDespesas.Domain.Interfaces.Authentication;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Authentication
{
    public class JWTAuthenticationTest : BaseTest
    {
        private readonly IJWTAuthentication _jwtAuthentication;

        public JWTAuthenticationTest()
        {
            _jwtAuthentication = new JWTAuthentication(MockSettingsAPI);
        }

        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerarTokenJwt()
        {
            var usuarioQR = new SettingsTest().UsuarioQR;

            var tokenJWT = _jwtAuthentication.GenerarTokenJwt(usuarioQR);

            TestContext.WriteLine(tokenJWT);

            Assert.IsNotNull(tokenJWT);
            Assert.IsNotEmpty(tokenJWT);
        }

        [TearDown]
        public void TearDown() { }
    }
}