namespace ControleDespesas.Dominio.Query
{
    public class CommandResult2<TEntity> where TEntity : class
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public TEntity Dados { get; set; }

        public CommandResult2(bool sucesso, string mensagem, TEntity dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}