using DesafioBancoDigital.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBancoDigital.Domain.Entites
{
    [Table("Contas")]
    public class Conta
    {
        [GraphQLName("conta")]
        [Key]
        public int NumeroConta { get; private set; }

        [GraphQLName("saldo")]
        [Column("SaldoConta")]
        public double SaldoConta { get; private set; }

        //metodo para sacar o valor da conta, verifica se o valor é valido e se o saldo é suficiente.
        public void Sacar(double valor)
        {
            if (valor <= 0)
            {
                throw new ValorInvalidoException(valor);
            }
            if (valor > SaldoConta)
            {
                throw new SaldoInsuficienteException(valor);
            }

            SaldoConta -= valor;
        }
        //metodo para depositar o valor da conta, verifica se o valor é valido.
        public void Depositar(double valor)
        {
            if (valor <= 0)
            {
                throw new ValorInvalidoException(valor);
            }
            SaldoConta += valor;
        }

        public Conta() { }
        public Conta(int numero, double saldo)
        {
            NumeroConta = numero;
            SaldoConta = saldo;
        }

    }
}
