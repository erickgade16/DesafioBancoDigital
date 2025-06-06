﻿using DesafioBancoDigital.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBancoDigital.Domain.Interface
{
    public interface IContaRepository
    {
        Task<Conta> ObterPorNumero(int numeroConta);
        Task AtualizarSaldo(Conta conta);
        Task<Conta> CriarConta(double saldoInicial);
    }
}
