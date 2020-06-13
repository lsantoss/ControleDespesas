using System.ComponentModel;

namespace ControleDespesas.Dominio.Enums
{
    public enum EPrivilegioUsuario
    {
        [Description("Admin")]
        Admin = 1,

        [Description("ReadOnly")]
        ReadOnly = 2
    }
}