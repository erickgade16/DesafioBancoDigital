using DesafioBancoDigital.Domain.Entites;
using DesafioBancoDigital.Domain.Exceptions;
using DesafioBancoDigital.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ContaServices
    {
        private readonly IContaRepository _contaRepository;

        public ContaServices(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<Conta> Sacar(int numeroConta, double valor)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            conta.Sacar(valor);
            await _contaRepository.AtualizarSaldo(conta);

            return conta;
        }

        public async Task<Conta> Depositar(int numeroConta, double valor)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            conta.Depositar(valor);
            await _contaRepository.AtualizarSaldo(conta);

            return conta;
        }

        public async Task<double> ObterSaldo(int numeroConta)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            return conta.SaldoConta;
        }
    }
}
