using Application;
using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;

namespace DesafioBancoDigital.API.GraphQL
{
    public class Mutation
    {
        // Realiza saque e trata erros esperados
        public async Task<Conta> Sacar(
            [GraphQLName("conta")] int numeroConta,
            decimal valor,
            [Service] ContaServices contaServices)
        {
            try
            {
                return await contaServices.Sacar(numeroConta, valor);
            }
            catch (ContaNaoEncontradaException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Conta nao encontrada")
                    .Build());
            }
            catch (SaldoInsuficienteException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Sem saldo")
                    .Build());
            }
            catch (ValorInvalidoException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Valor invalido")
                    .Build());
            }
        }

        // Realiza depósito e trata erros esperados
        public async Task<Conta> Depositar(
            [GraphQLName("conta")] int numeroConta,
            decimal valor,
            [Service] ContaServices contaServices)
        {
            try
            {
                return await contaServices.Depositar(numeroConta, valor);
            }
            catch (ContaNaoEncontradaException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Conta nao encontrada")
                    .Build());
            }
            catch (ValorInvalidoException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Valor invalido")
                    .Build());
            }
        }
    }
}
