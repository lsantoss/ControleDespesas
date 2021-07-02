using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Commands.Output;
using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Pessoas.Helpers
{
    public static class PessoaHelper
    {
        public static Pessoa GerarEntidade(AdicionarPessoaCommand command)
        {
            Pessoa pessoa = new Pessoa(
                0,
                new Usuario(command.IdUsuario),
                command.Nome,
                command.ImagemPerfil);

            pessoa.Validar();
            return pessoa;
        }

        public static Pessoa GerarEntidade(AtualizarPessoaCommand command)
        {
            Pessoa pessoa = new Pessoa(
                command.Id,
                new Usuario(command.IdUsuario),
                command.Nome,
                command.ImagemPerfil);

            pessoa.Validar();
            return pessoa;
        }

        public static PessoaCommandOutput GerarDadosRetornoInsert(Pessoa pessoa)
        {
            return new PessoaCommandOutput
            {
                Id = pessoa.Id,
                IdUsuario = pessoa.Usuario.Id,
                Nome = pessoa.Nome,
                ImagemPerfil = pessoa.ImagemPerfil
            };
        }

        public static PessoaCommandOutput GerarDadosRetornoUpdate(Pessoa pessoa)
        {
            return new PessoaCommandOutput
            {
                Id = pessoa.Id,
                IdUsuario = pessoa.Usuario.Id,
                Nome = pessoa.Nome,
                ImagemPerfil = pessoa.ImagemPerfil
            };
        }

        public static CommandOutput GerarDadosRetornoDelete(int id)
        {
            return new CommandOutput(id);
        }
    }
}