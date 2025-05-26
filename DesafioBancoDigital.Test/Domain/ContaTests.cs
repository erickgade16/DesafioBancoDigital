using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using Xunit;

namespace DesafioBancoDigital.Tests.Domain
{
    public class ContaTests
    {
        [Fact]
        public void ReduzirSaldo()
        {
            var conta = new Conta(1, 1000);
            conta.Sacar(200);
            Assert.Equal(800, conta.SaldoConta);
        }

        [Fact]
        public void AumentarSaldo()
        {
            var conta = new Conta(1, 100);
            conta.Depositar(250);
            Assert.Equal(350, conta.SaldoConta);
        }

        [Fact]
        public void SacarValorInvalido()
        {
            var conta = new Conta(1, 1000);
            Assert.Throws<ValorInvalidoException>(() => conta.Sacar(-100));
        }

        [Fact]
        public void SacarValorMaiorQueSaldo()
        {
            var conta = new Conta(1, 1000);
            Assert.Throws<SaldoInsuficienteException>(() => conta.Sacar(2000));
        }

        [Fact]
        public void DepositarValorInvalido()
        {
            var conta = new Conta(1, 1000);
            Assert.Throws<ValorInvalidoException>(() => conta.Depositar(-100));
        }

        [Fact]
        public void CriarContaComValoresIniciais()
        {
            var conta = new Conta(1, 1000);
            Assert.Equal(1, conta.NumeroConta);
            Assert.Equal(1000, conta.SaldoConta);
        }
    }
}
