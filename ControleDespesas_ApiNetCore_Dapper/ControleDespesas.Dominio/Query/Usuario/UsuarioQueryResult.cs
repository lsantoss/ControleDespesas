﻿using ControleDespesas.Dominio.Enums;

namespace ControleDespesas.Dominio.Query.Usuario
{
    public class UsuarioQueryResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
    }
}