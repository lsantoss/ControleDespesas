namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class LogRequestResponseQueries
    {
        public static string Salvar { get; } = @"INSERT INTO LogRequestResponse
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

        public static string Obter { get; } = @"SELECT 
                                                    LogRequestResponseId AS LogRequestResponseId, 
                                                    MachineName AS MachineName,
                                                    DataRequest AS DataRequest,
                                                    DataResponse AS DataResponse,
                                                    EndPoint AS EndPoint,
                                                    Request AS Request,
                                                    Response AS Response,
                                                    TempoDuracao AS TempoDuracao
                                                FROM LogRequestResponse 
                                                WHERE LogRequestResponseId = @LogRequestResponseId";

        public static string Listar { get; } = @"SELECT 
                                                    LogRequestResponseId AS LogRequestResponseId, 
                                                    MachineName AS MachineName,
                                                    DataRequest AS DataRequest,
                                                    DataResponse AS DataResponse,
                                                    EndPoint AS EndPoint,
                                                    Request AS Request,
                                                    Response AS Response,
                                                    TempoDuracao AS TempoDuracao
                                                FROM LogRequestResponse  
                                                ORDER BY LogRequestResponseId ASC";
    }
}