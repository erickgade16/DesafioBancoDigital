﻿using Application;
using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;

namespace DesafioBancoDigital.API.GraphQL
{
    public class Mutation
    {
        public async Task<Conta> CriarConta(
            [GraphQLName("saldoInicial")] double saldoInicial,
            [Service] ContaServices contaServices)
        {
            try
            {
                return await contaServices.CriarConta(saldoInicial);
            }
            catch (ValorInvalidoException ex)
            {
                throw new GraphQLException(ErrorBuilder.New()
                    .SetMessage(ex.Message)
                    .SetCode("Valor invalido")
                    .Build());
            }
        }

        public async Task<Conta> Sacar(
            [GraphQLName("conta")] int numeroConta,
            double valor,
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

        public async Task<Conta> Depositar(
            [GraphQLName("conta")] int numeroConta,
            double valor,
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
