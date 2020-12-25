using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pessoa.Input
{
    public class AtualizarPessoaCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string ImagemPerfil { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Nome, "Nome", "Nome é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(ImagemPerfil, "Imagem de Perfil", "Imagem de Perfil é um campo obrigatório"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}