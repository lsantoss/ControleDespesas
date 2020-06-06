using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using Xunit;

namespace ControleDespesas.Testes.Entidades
{
    public class EmpresaTest
    {
        private readonly Empresa _empresaTeste;

        public EmpresaTest()
        {
            int id = 1;
            Descricao100Caracteres nome = new Descricao100Caracteres("Oi", "Nome");
            string logo = "base64String";
            _empresaTeste = new Empresa(id, nome, logo);
        }

        [Fact]
        public void ValidarEntidade_Valida()
        {
            Empresa empresa = _empresaTeste;
            int resultado = empresa.Notificacoes.Count;
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void ValidarEntidade_NomeInvalido()
        {
            string nomeLongo = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            Empresa empresa = new Empresa(
                _empresaTeste.Id,
                new Descricao100Caracteres(nomeLongo, "Nome"),
                _empresaTeste.Logo
            );

            bool resultado = empresa.Nome.Valido;
            Assert.False(resultado);
        }
    }
}