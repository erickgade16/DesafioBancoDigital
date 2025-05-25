namespace DesafioBancoDigital.Domain.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException(double valor)
            : base($"Saldo insuficiente para sacar {valor:C}")
        {
        }
    }
} 