using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Pessoas.Helpers
{
    public static class PessoaHelper
    {
        public static Pessoa GerarEntidade(AdicionarPessoaCommand command)
        {
            return new Pessoa(command.IdUsuario, command.Nome, command.ImagemPerfil);
        }

        public static Pessoa GerarEntidade(AtualizarPessoaCommand command)
        {
            return new Pessoa(command.Id, command.IdUsuario, command.Nome, command.ImagemPerfil);
        }

        public static PessoaCommandOutput GerarDadosRetorno(Pessoa pessoa)
        {
            return new PessoaCommandOutput
            {
                Id = pessoa.Id,
                IdUsuario = pessoa.IdUsuario,
                Nome = pessoa.Nome,
                ImagemPerfil = pessoa.ImagemPerfil
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(long id)
        {
            return new CommandOutput(id);
        }
    }
}