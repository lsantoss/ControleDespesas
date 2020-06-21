using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Entidades;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class PessoaHelper
    {
        public static Pessoa GerarEntidade(AdicionarPessoaCommand command)
        {
            try
            {
                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(0, nome, imagemPerfil);
                return pessoa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Pessoa GerarEntidade(AtualizarPessoaCommand command)
        {
            try
            {
                int id = command.Id;
                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(id, nome, imagemPerfil);
                return pessoa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(Pessoa pessoa)
        {
            try
            {
                return new
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome.ToString(),
                    ImagemPerfil = pessoa.ImagemPerfil
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}