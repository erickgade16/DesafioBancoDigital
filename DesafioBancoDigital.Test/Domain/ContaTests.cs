using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using System;
using Xunit;

namespace DesafioBancoDigital.Tests.Domain
{
    public class ContaTests
    {
        //Reduzir o saldo da conta, verificando se o valor é valido e se o saldo é suficiente.
        [Fact]
        public void ReduzirSaldo ()
        {
           
            var conta = new Conta(1, 1000);

            conta.Sacar(200);

            Assert.Equal(800, conta.SaldoConta);
        }

        //Aumentar o saldo da conta, verificando se o valor é valido.
        [Fact]
        public void AumentarSaldo()
        {
            var conta = new Conta(1, 100);

            conta.Depositar(250);

            Assert.Equal(350, conta.SaldoConta);
        }
    }
}
