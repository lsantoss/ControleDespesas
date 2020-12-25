using System.ComponentModel;

namespace ControleDespesas.Dominio.Enums
{
    public enum EPrivilegioUsuario
    {
        [Description("Administrador")]
        Admin = 1,

        [Description("Somente Leitura")]
        ReadOnly = 2
    }
}