using Application;
using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Interface;
using Moq;

namespace DesafioBancoDigital.Test.Application
{
    public class ContaServicesTests
    {
        //Teste servico de saque.
        [Fact]
        public async Task SacarTest()
        {

            var conta = new Conta(1, 1000);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync(conta);
            repoMock.Setup(r => r.AtualizarSaldo(conta)).Returns(Task.CompletedTask);

            var service = new ContaServices(repoMock.Object);

            
            var resultado = await service.Sacar(1, 200);

            
            Assert.Equal(800, resultado.SaldoConta);
        }

        //Teste servico de deposito.
        [Fact]
        public async Task DepositarTest()
        {
            var conta = new Conta(2, 500);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(2)).ReturnsAsync(conta);
            repoMock.Setup(r => r.AtualizarSaldo(conta)).Returns(Task.CompletedTask);

            var service = new ContaServices(repoMock.Object);

            var resultado = await service.Depositar(2, 300);

            Assert.Equal(800, resultado.SaldoConta);
        }

        // Testa o serviço ObterSaldo
        [Fact]
        public async Task ObterSaldoTest()
        {
            var conta = new Conta(3, 1200);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(3)).ReturnsAsync(conta);

            var service = new ContaServices(repoMock.Object);

            var saldo = await service.ObterSaldo(3);

            Assert.Equal(1200, saldo);
        }

    }
}
