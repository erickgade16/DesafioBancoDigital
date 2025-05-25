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
        //Servico para sacar e verificar se conta existe, se o valor é valido e se o saldo é suficiente.
        public async Task<Conta> Sacar(int numeroConta, decimal valor)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            conta.Sacar(valor);
            await _contaRepository.AtualizarSaldo(conta);

            return conta;
        }
        //Servico para depositar e verificar se conta existe, se o valor é valido.
        public async Task<Conta> Depositar(int numeroConta, decimal valor)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);

            conta.Depositar(valor);
            await _contaRepository.AtualizarSaldo(conta);

            return conta;
        }

        //Servico para Obter saldo de uma conta com numero e verifica se a conta existe.
        public async Task<decimal> ObterSaldo(int numeroConta)
        {
            var conta = await _contaRepository.ObterPorNumero(numeroConta);

            if (conta == null)
                throw new ContaNaoEncontradaException(numeroConta);


            return conta.SaldoConta;
        }


    }
}
