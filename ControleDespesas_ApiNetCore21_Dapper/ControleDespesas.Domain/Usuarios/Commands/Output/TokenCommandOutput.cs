namespace ControleDespesas.Domain.Usuarios.Commands.Output
{
    public class TokenCommandOutput
    {
        public string Token { get; set; }

        public TokenCommandOutput(string token)
        {
            Token = token;
        }
    }
}