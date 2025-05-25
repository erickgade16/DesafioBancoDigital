namespace DesafioBancoDigital.Domain.Exceptions
{
    public class ContaNaoEncontradaException : Exception
    {
        public ContaNaoEncontradaException(int numeroConta)
            : base($"Conta de número {numeroConta} não foi encontrada.")
        {
        }
    }
}
