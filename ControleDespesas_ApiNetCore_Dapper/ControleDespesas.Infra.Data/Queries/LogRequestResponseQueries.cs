namespace ControleDespesas.Infra.Data.Queries
{
    public static class LogRequestResponseQueries
    {
        public static string Adicionar { get; } = @"INSERT INTO LogRequestResponse
                                                        (MachineName
                                                        ,DataRequest
                                                        ,DataResponse
                                                        ,EndPoint
                                                        ,Request
                                                        ,Response
                                                        ,TempoDuracao)
                                                    VALUES
                                                        (@MachineName
                                                        ,@DataRequest
                                                        ,@DataResponse
                                                        ,@EndPoint
                                                        ,@Request
                                                        ,@Response
                                                        ,@TempoDuracao)";
    }
}