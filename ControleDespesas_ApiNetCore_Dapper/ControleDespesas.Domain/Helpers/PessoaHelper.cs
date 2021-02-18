﻿using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Entities;
using System;

namespace ControleDespesas.Domain.Helpers
{
    public static class PessoaHelper
    {
        public static Pessoa GerarEntidade(AdicionarPessoaCommand command)
        {
            try
            {
                Usuario usuario = new Usuario(command.IdUsuario);
                string nome = command.Nome;
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(0, usuario, nome, imagemPerfil);
                pessoa.Validar();
                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Pessoa GerarEntidade(AtualizarPessoaCommand command)
        {
            try
            {
                int id = command.Id;
                Usuario usuario = new Usuario(command.IdUsuario);
                string nome = command.Nome;
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(id, usuario, nome, imagemPerfil);
                pessoa.Validar();
                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static AdicionarPessoaCommandOutput GerarDadosRetornoInsert(Pessoa pessoa)
        {
            try
            {
                return new AdicionarPessoaCommandOutput
                {
                    Id = pessoa.Id,
                    IdUsuario = pessoa.Usuario.Id,
                    Nome = pessoa.Nome,
                    ImagemPerfil = pessoa.ImagemPerfil
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static AtualizarPessoaCommandOutput GerarDadosRetornoUpdate(Pessoa pessoa)
        {
            try
            {
                return new AtualizarPessoaCommandOutput
                {
                    Id = pessoa.Id,
                    IdUsuario = pessoa.Usuario.Id,
                    Nome = pessoa.Nome,
                    ImagemPerfil = pessoa.ImagemPerfil
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static ApagarPessoaCommandOutput GerarDadosRetornoDelete(int id)
        {
            try
            {
                return new ApagarPessoaCommandOutput { Id = id };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}