namespace ControleDespesas.Infra.Data.Queries
{
    public static class LogRequestResponseQueries
    {
        public static string Adicionar { get; } = @"INSERT INTO LogRequestResponse
                                                        (MachineName
                                                        ,DataEnvio
                                                        ,DataRecebimento
                                                        ,EndPoint
                                                        ,Request
                                                        ,Response
                                                        ,TempoDuracao)
                                                    VALUES
                                                        (@MachineName
                                                        ,@DataEnvio
                                                        ,@DataRecebimento
                                                        ,@EndPoint
                                                        ,@Request
                                                        ,@Response
                                                        ,@TempoDuracao)";
    }
}