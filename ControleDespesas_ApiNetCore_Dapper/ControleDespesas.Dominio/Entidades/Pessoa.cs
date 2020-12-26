﻿using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pessoa : Notificadora
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Texto Nome { get; set; }
        public string ImagemPerfil { get; set; }

        public Pessoa(int id, Usuario usuario, Texto nome, string imagemPerfil)
        {
            Id = id;
            Usuario = usuario;
            Nome = nome;
            ImagemPerfil = imagemPerfil;
        }

        public Pessoa(int id) => Id = id;
    }
}