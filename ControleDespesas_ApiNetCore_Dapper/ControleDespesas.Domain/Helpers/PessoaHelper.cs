using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Entities;

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

        public static AdicionarPessoaCommandOutput GerarDadosRetornoInsert(Pessoa pessoa)
        {
            return new AdicionarPessoaCommandOutput
            {
                Id = pessoa.Id,
                IdUsuario = pessoa.Usuario.Id,
                Nome = pessoa.Nome,
                ImagemPerfil = pessoa.ImagemPerfil
            };
        }

        public static AtualizarPessoaCommandOutput GerarDadosRetornoUpdate(Pessoa pessoa)
        {
            return new AtualizarPessoaCommandOutput
            {
                Id = pessoa.Id,
                IdUsuario = pessoa.Usuario.Id,
                Nome = pessoa.Nome,
                ImagemPerfil = pessoa.ImagemPerfil
            };
        }

        public static ApagarPessoaCommandOutput GerarDadosRetornoDelete(int id)
        {
            return new ApagarPessoaCommandOutput { Id = id };
        }
    }
}