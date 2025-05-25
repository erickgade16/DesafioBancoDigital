namespace DesafioBancoDigital.Domain.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException(decimal valor)
            : base($"Saldo insuficiente para realizar a operação de R$ {valor:F2}.")
        {
        }
    }
}