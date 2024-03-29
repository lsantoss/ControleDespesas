﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ControleDespesas.Test.AppConfigurations.Util
{
    public static class FormatadorJson
    {
        public static string FormatarJsonDeSaida<TEntity>(this string json)
        {
            var options = new JsonSerializerOptions() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            var objeto = JsonSerializer.Deserialize<TEntity>(json);
            var retorno = JsonSerializer.Serialize(objeto, options);
            return retorno;
        }

        public static string FormatarJsonDeSaida<TEntity>(this TEntity objeto)
        {
            var options = new JsonSerializerOptions() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            var retorno = JsonSerializer.Serialize(objeto, options);
            return retorno;
        }
    }
}