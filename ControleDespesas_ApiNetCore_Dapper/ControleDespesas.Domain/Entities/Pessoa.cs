﻿using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Entities
{
    public class Pessoa : Notificadora
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }

        public Pessoa(int id, Usuario usuario, string nome, string imagemPerfil)
        {
            Id = id;
            Usuario = usuario;
            Nome = nome;
            ImagemPerfil = imagemPerfil;
        }

        public Pessoa(int id)
        {
            Id = id;
        }

        public void Validar()
        {
            try
            {
                if (Usuario.Id <= 0)
                    AddNotificacao("Id do Usuário", "Id do Usuário não é valido");

                if (string.IsNullOrEmpty(Nome))
                    AddNotificacao("Nome", "Nome é um campo obrigatório");
                else if (Nome.Length > 100)
                    AddNotificacao("Nome", "Nome maior que o esperado");

                if (string.IsNullOrEmpty(ImagemPerfil))
                    AddNotificacao("ImagemPerfil", "ImagemPerfil é um campo obrigatório");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}