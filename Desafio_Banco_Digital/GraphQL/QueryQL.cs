using Application;
using DesafioBancoDigital.Domain.Exceptions;

namespace DesafioBancoDigital.API.GraphQL
{
    public class QueryQL
    {
        public async Task<decimal> Saldo(
            [GraphQLName("conta")] int numeroConta,
            [Service] ContaServices contaServices)
        {
            try
            {
                return await contaServices.ObterSaldo(numeroConta);
            }
            catch (ContaNaoEncontradaException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Conta nao encontrada")
                    .Build());
            }
            catch (Exception)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage("Erro inesperado ao obter saldo.")
                    .SetCode("Erro interno")
                    .Build());
            }
        }
    }
}
