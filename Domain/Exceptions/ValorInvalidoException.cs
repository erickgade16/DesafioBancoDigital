namespace DesafioBancoDigital.Domain.Exceptions
{
    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException(double valor)
            : base($"O valor {valor:C} é inválido para a operação")
        {
        }
    }
}