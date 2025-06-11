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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumeroConta { get; private set; }

        [GraphQLName("saldo")]
        [Column("SaldoConta")]
        public double SaldoConta { get; private set; }

        //teste


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
