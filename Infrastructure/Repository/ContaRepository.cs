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
        //Pegar primeira conta que tem o numero igual ao numero da conta que foi passada como parametro.
        // Verifica se a conta existe, se não existir lança uma exceção.
        public async Task<Conta> ObterPorNumero(int numeroConta)
        {
            var conta = await _context.Contas
                .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            return conta;
        }
        //Atualiza o saldo da conta que foi passada como parametro.
        public async Task AtualizarSaldo(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }
       
    }
}