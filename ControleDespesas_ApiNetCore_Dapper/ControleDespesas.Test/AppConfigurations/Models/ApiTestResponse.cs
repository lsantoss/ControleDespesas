namespace ControleDespesas.Test.AppConfigurations.Models
{
    public class ApiTestResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public T Value { get; set; }
    }
}