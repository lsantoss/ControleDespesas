using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Commands.Pessoa.Input
{
    public class AtualizarPessoaCommand : Notificadora, CommandPadrao 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Nome, 1, "Nome", "Nome é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(ImagemPerfil, 1, "Imagem de Perfil", "Imagem de Perfil é um campo obrigatório"));

            return Valido;
        }
    }
}