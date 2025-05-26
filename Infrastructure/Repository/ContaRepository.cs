using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using DesafioBancoDigital.Domain.Interface;
using DesafioBancoDigital.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace DesafioBancoDigital.Infrastructure.Repository
{
    public class ContaRepository : IContaRepository
    {
        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Conta> ObterPorNumero(int numeroConta)
        {
            var conta = await _context.Contas
                .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            return conta;
        }

        public async Task AtualizarSaldo(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }

    }
}