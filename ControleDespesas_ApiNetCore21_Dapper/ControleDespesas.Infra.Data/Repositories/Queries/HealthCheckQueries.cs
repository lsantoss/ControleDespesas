namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class HealthCheckQueries
    {
        public static string VerificarELMAH_Error { get; } = @"SELECT TOP 1 1 FROM ELMAH_Error WITH(NOLOCK);";

        public static string VerificarLogRequestResponse { get; } = @"SELECT TOP 1 1 FROM LogRequestResponse WITH(NOLOCK);";

        public static string VerificarTipoPagamento { get; } = @"SELECT TOP 1 1 FROM TipoPagamento WITH(NOLOCK);";

        public static string VerificarEmpresa { get; } = @"SELECT TOP 1 1 FROM Empresa WITH(NOLOCK);";

        public static string VerificarUsuario { get; } = @"SELECT TOP 1 1 FROM Usuario WITH(NOLOCK);";

        public static string VerificarPessoa { get; } = @"SELECT TOP 1 1 FROM Pessoa WITH(NOLOCK);";

        public static string VerificarPagamento { get; } = @"SELECT TOP 1 1 FROM Pagamento WITH(NOLOCK);";
    }
}