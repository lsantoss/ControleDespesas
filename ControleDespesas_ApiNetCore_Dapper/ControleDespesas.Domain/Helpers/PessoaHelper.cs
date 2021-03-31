using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Infra.Commands;

namespace ControleDespesas.Domain.Helpers
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
            return new CommandOutput { Id = id };
        }
    }
}