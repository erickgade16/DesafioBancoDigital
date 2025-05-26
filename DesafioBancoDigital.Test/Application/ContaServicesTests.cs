using Application;
using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using DesafioBancoDigital.Domain.Interface;
using Moq;
using Xunit;

namespace DesafioBancoDigital.Test.Application
{
    public class ContaServicesTests
    {
        [Fact]
        public async Task CriarContaTest()
        {
            var saldoInicial = 1000.0;
            var numeroConta = 1;
            var novaConta = new Conta(numeroConta, saldoInicial);
            
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.CriarConta(saldoInicial)).ReturnsAsync(novaConta);

            var service = new ContaServices(repoMock.Object);
            var resultado = await service.CriarConta(saldoInicial);

            Assert.Equal(numeroConta, resultado.NumeroConta);
            Assert.Equal(saldoInicial, resultado.SaldoConta);
        }

        [Fact]
        public async Task CriarContaComSaldoNegativo()
        {
            var saldoInicial = -100.0;
            var repoMock = new Mock<IContaRepository>();
            var service = new ContaServices(repoMock.Object);

            await Assert.ThrowsAsync<ValorInvalidoException>(() => 
                service.CriarConta(saldoInicial));
        }

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

        [Fact]
        public async Task SacarContaNaoEncontrada()
        {
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync((Conta)null);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() => service.Sacar(1, 200));
        }

        [Fact]
        public async Task DepositarContaNaoEncontrada()
        {
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync((Conta)null);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() => service.Depositar(1, 200));
        }

        [Fact]
        public async Task ObterSaldoContaNaoEncontrada()
        {
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync((Conta)null);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() => service.ObterSaldo(1));
        }

        [Fact]
        public async Task SacarValorInvalido()
        {
            var conta = new Conta(1, 1000);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync(conta);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<ValorInvalidoException>(() => service.Sacar(1, -100));
        }

        [Fact]
        public async Task DepositarValorInvalido()
        {
            var conta = new Conta(1, 1000);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync(conta);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<ValorInvalidoException>(() => service.Depositar(1, -100));
        }

        [Fact]
        public async Task SacarSaldoInsuficiente()
        {
            var conta = new Conta(1, 1000);
            var repoMock = new Mock<IContaRepository>();
            repoMock.Setup(r => r.ObterPorNumero(1)).ReturnsAsync(conta);

            var service = new ContaServices(repoMock.Object);
            await Assert.ThrowsAsync<SaldoInsuficienteException>(() => service.Sacar(1, 2000));
        }
    }
}
