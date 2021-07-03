using ControleDespesas.Infra.Settings;
using ControleDespesas.Test.AppConfigurations.Settings;
using NUnit.Framework;

namespace ControleDespesas.Test.AppConfigurations.Base
{
    public class BaseTest
    {
        protected SettingsTest MockSettingsTest { get; }
        protected SettingsApi MockSettingsApi { get; }

        public BaseTest()
        {
            MockSettingsTest = new SettingsTest();

            MockSettingsApi = new SettingsApi()
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