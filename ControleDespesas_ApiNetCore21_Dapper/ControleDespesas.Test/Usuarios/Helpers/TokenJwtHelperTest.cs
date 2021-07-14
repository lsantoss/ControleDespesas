using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Test.AppConfigurations.Base;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ControleDespesas.Test.Usuarios.Helpers
{
    [TestFixture]
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

            var userTokenJWT = new JwtSecurityTokenHandler().ReadToken(tokenJWT) as JwtSecurityToken;

            var unique_nameValido = userTokenJWT.Claims.Any(claim => claim.Type == "unique_name" && claim.Value == "lucas@123");
            var roleValido = userTokenJWT.Claims.Any(claim => claim.Type == "role" && claim.Value == "Administrador");

            TestContext.WriteLine(tokenJWT);
            TestContext.WriteLine();
            TestContext.WriteLine(userTokenJWT);

            Assert.True(unique_nameValido);
            Assert.True(roleValido);
        }

        [TearDown]
        public void TearDown() { }
    }
}