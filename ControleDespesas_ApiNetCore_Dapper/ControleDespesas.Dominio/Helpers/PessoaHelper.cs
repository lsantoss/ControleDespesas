﻿using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
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
                Usuario usuario = new Usuario(command.IdUsuario);
                Texto nome = new Texto(command.Nome, "Nome", 100);
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(0, usuario, nome, imagemPerfil);
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
                Usuario usuario = new Usuario(command.IdUsuario);
                Texto nome = new Texto(command.Nome, "Nome", 100);
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(id, usuario, nome, imagemPerfil);
                return pessoa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                    Nome = pessoa.Nome.ToString(),
                    ImagemPerfil = pessoa.ImagemPerfil
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                    Nome = pessoa.Nome.ToString(),
                    ImagemPerfil = pessoa.ImagemPerfil
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }
        }
    }
}