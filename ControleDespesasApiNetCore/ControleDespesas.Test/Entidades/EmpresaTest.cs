﻿using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Test.AppConfigurations.Factory;
using LSCode.Validador.ValueObjects;
using NUnit.Framework;

namespace ControleDespesas.Test.Entidades
{
    public class EmpresaTest : BaseTest
    {
        private Empresa _empresa;

        [SetUp]
        public void Setup() => _empresa = new EmpresaTest().MockSettingsTest.Empresa1;

        [Test]
        public void ValidarEntidade_Valida()
        {
            Assert.True(_empresa.Valido);
            Assert.True(_empresa.Nome.Valido);
            Assert.AreEqual(0, _empresa.Notificacoes.Count);
            Assert.AreEqual(0, _empresa.Nome.Notificacoes.Count);
        }

        [Test]
        public void ValidarEntidade_NomeInvalido()
        {
            _empresa.Nome = new Texto("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Nome", 100);

            Assert.False(_empresa.Nome.Valido);
            Assert.AreNotEqual(0, _empresa.Nome.Notificacoes.Count);
        }

        [TearDown]
        public void TearDown() => _empresa = null;
    }
}