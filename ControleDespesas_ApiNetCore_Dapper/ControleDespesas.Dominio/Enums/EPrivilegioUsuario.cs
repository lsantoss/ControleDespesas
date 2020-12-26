﻿using System.ComponentModel;

namespace ControleDespesas.Dominio.Enums
{
    public enum EPrivilegioUsuario
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Escrita")]
        Escrita = 2,

        [Description("Somente Leitura")]
        SomenteLeitura = 3
    }
}