using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using DesafioBancoDigital.Infrastructure.Context;
using DesafioBancoDigital.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DesafioBancoDigital.Test.Infrastructure
{
    public class ContaRepositoryTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB" + Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task ObterPorNumero_ContaExistente()
        {
            using var context = CreateContext();
            var conta = new Conta(1, 1000);
            context.Contas.Add(conta);
            await context.SaveChangesAsync();

            var repository = new ContaRepository(context);
            var resultado = await repository.ObterPorNumero(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.NumeroConta);
            Assert.Equal(1000, resultado.SaldoConta);
        }

        [Fact]
        public async Task ObterPorNumero_ContaNaoExistente()
        {
            using var context = CreateContext();
            var repository = new ContaRepository(context);

            await Assert.ThrowsAsync<ContaNaoEncontradaException>(() => 
                repository.ObterPorNumero(1));
        }

        [Fact]
        public async Task AtualizarSaldo_ContaExistente()
        {
            using var context = CreateContext();
            var conta = new Conta(1, 1000);
            context.Contas.Add(conta);
            await context.SaveChangesAsync();

            var repository = new ContaRepository(context);
            conta.Depositar(500);
            await repository.AtualizarSaldo(conta);

            var contaAtualizada = await context.Contas.FindAsync(conta.NumeroConta);
            Assert.Equal(1500, contaAtualizada.SaldoConta);
        }
    }
} 