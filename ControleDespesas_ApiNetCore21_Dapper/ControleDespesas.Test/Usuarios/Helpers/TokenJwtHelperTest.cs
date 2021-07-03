using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.Usuarios.Helpers
{
    public class TokenJwtHelperTest : BaseTest
    {
        private readonly ITokenJwtHelper _tokenJwtHelper;

        public TokenJwtHelperTest()
        {
            _tokenJwtHelper = new TokenJwtHelper(MockSettingsApi);
        }

        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerarTokenJwt()
        {
            var usuarioQR = new SettingsTest().UsuarioQR;

            var tokenJWT = _tokenJwtHelper.GenerarTokenJwt(usuarioQR);

            TestContext.WriteLine(tokenJWT);

            Assert.IsNotNull(tokenJWT);
            Assert.IsNotEmpty(tokenJWT);
        }

        [TearDown]
        public void TearDown() { }
    }
}