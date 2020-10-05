using ControleDespesas.Api.Settings;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.AppConfigurations.Factory
{
    [TestFixture]
    public class BaseTest
    {
        protected SettingsTest MockSettingsTest { get; }
        protected SettingsAPI MockSettingsAPI { get; }

        public BaseTest()
        {
            MockSettingsTest = new SettingsTest();

            MockSettingsAPI = new SettingsAPI()
            {
                ControleDespesasAPINetCore = MockSettingsTest.ControleDespesasAPINetCore,
                ChaveAPI = MockSettingsTest.ChaveAPI,
                ChaveJWT = MockSettingsTest.ChaveJWT
            };
        }

        [OneTimeSetUp]
        private void OneTimeSetUp() { }

        [OneTimeTearDown]
        private void OneTimeTearDown() { }
    }
}