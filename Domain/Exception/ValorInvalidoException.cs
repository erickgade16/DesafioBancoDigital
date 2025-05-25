namespace DesafioBancoDigital.Domain.Exceptions
{
    public class ValorInvalidoException : Exception
    {
        public ValorInvalidoException(decimal valor)
            : base($"O valor R$ {valor:F2} é inválido para esta operação.")
        {
        }

        public ValorInvalidoException(string message) : base(message) { }

        public ValorInvalidoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

