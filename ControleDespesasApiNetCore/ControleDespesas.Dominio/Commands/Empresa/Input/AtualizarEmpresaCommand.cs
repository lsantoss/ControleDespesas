using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Commands.Empresa.Input
{
    public class AtualizarEmpresaCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Nome, "Nome", "Nome é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Logo, "Logo", "Logo é um campo obrigatório"));

                return Valido;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}