using System.Text.Json;

namespace ControleDespesas.Test.AppConfigurations.Util
{
    public static class FotmatadorJson
    {
        public static string FormatarJsonDeSaida<TEntity>(string json)
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var objeto = JsonSerializer.Deserialize<TEntity>(json);
            var retorno = JsonSerializer.Serialize(objeto, options);
            return retorno;
        }
    }
}