using System;

namespace ControleDespesas.Infra.Logs
{
    public class LogRequestResponse
    {
        public int LogRequestResponseId { get; set; }
        public string MachineName { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataRecebimento { get; set; }
        public string EndPoint { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public long? TempoDuracao { get; set; }
    }
}