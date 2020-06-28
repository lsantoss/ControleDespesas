namespace ControleDespesas.Dominio.Query
{
    public interface ICommandResult2<TEntity> where TEntity : class
    {
        bool Sucesso { get; set; }
        string Mensagem { get; set; }
        TEntity Dados { get; set; }
    }
}