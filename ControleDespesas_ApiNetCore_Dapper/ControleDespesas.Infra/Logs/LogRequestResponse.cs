using System;

namespace ControleDespesas.Infra.Logs
{
    public class LogRequestResponse
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTime DataRequest { get; set; }
        public DateTime DataResponse { get; set; }
        public string EndPoint { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public long? TempoDuracao { get; set; }
    }
}