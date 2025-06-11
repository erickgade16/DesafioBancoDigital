import { useState } from 'react';
import { useMutation, useQuery, gql } from '@apollo/client';

const CRIAR_CONTA = gql`
  mutation CriarConta($saldoInicial: Float!) {
    criarConta(saldoInicial: $saldoInicial) {
      conta
      saldo
    }
  }
`;

const SACAR = gql`
  mutation Sacar($numeroConta: Int!, $valor: Float!) {
    sacar(conta: $numeroConta, valor: $valor) {
      conta
      saldo
    }
  }
`;

const DEPOSITAR = gql`
  mutation Depositar($numeroConta: Int!, $valor: Float!) {
    depositar(conta: $numeroConta, valor: $valor) {
      conta
      saldo
    }
  }
`;

const OBTER_SALDO = gql`
  query ObterSaldo($numeroConta: Int!) {
    saldo(conta: $numeroConta)
  }
`;

export default function ContaManager() {
  const [numeroConta, setNumeroConta] = useState('');
  const [valor, setValor] = useState('');
  const [saldo, setSaldo] = useState(null);
  const [mensagem, setMensagem] = useState('');

  const [criarConta] = useMutation(CRIAR_CONTA);
  const [sacar] = useMutation(SACAR);
  const [depositar] = useMutation(DEPOSITAR);
  const { refetch: refetchSaldo } = useQuery(OBTER_SALDO, {
    variables: { numeroConta: parseInt(numeroConta) },
    skip: !numeroConta,
  });

  const handleCriarConta = async () => {
    if (isNaN(parseFloat(valor)) || parseFloat(valor) <= 0) {
      setMensagem('Por favor, insira um valor inicial válido para a conta.');
      return;
    }
    try {
      const { data } = await criarConta({
        variables: { saldoInicial: parseFloat(valor) }
      });
      setMensagem(`Conta criada com sucesso! Número: ${data.criarConta.conta}`);
      setNumeroConta(data.criarConta.conta.toString());
      setValor('');
    } catch (error) {
      setMensagem(`Erro ao criar conta: ${error.message}`);
    }
  };

  const handleSacar = async () => {
    if (isNaN(parseInt(numeroConta)) || isNaN(parseFloat(valor)) || parseFloat(valor) <= 0) {
      setMensagem('Por favor, insira o número da conta e um valor válido para o saque.');
      return;
    }
    try {
      await sacar({
        variables: {
          numeroConta: parseInt(numeroConta),
          valor: parseFloat(valor)
        }
      });
      const { data } = await refetchSaldo();
      setSaldo(data.saldo);
      setMensagem('Saque realizado com sucesso!');
      setValor('');
    } catch (error) {
      setMensagem(`Erro ao sacar: ${error.message}`);
    }
  };

  const handleDepositar = async () => {
    if (isNaN(parseInt(numeroConta)) || isNaN(parseFloat(valor)) || parseFloat(valor) <= 0) {
      setMensagem('Por favor, insira o número da conta e um valor válido para o depósito.');
      return;
    }
    try {
      await depositar({
        variables: {
          numeroConta: parseInt(numeroConta),
          valor: parseFloat(valor)
        }
      });
      const { data } = await refetchSaldo();
      setSaldo(data.saldo);
      setMensagem('Depósito realizado com sucesso!');
      setValor('');
    } catch (error) {
      setMensagem(`Erro ao depositar: ${error.message}`);
    }
  };

  const handleConsultarSaldo = async () => {
    if (isNaN(parseInt(numeroConta))) {
      setMensagem('Por favor, insira o número da conta para consultar o saldo.');
      return;
    }
    try {
      const { data } = await refetchSaldo();
      setSaldo(data.saldo);
      setMensagem('Saldo consultado com sucesso!');
    } catch (error) {
      setMensagem(`Erro ao consultar saldo: ${error.message}`);
    }
  };

  const buttonClass = "w-full py-3 px-4 rounded-lg text-white font-semibold transition duration-300 ease-in-out transform hover:scale-105";
  const inputClass = "w-full p-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500";
  const messageClass = "p-3 rounded-lg text-sm text-center";

  return (
    <div className="space-y-6 max-w-3xl mx-auto p-6 bg-white rounded-lg shadow-xl">
      <h2 className="text-3xl font-semibold text-gray-700 text-center mb-6">Gerenciador de Conta</h2>
      
      <div className="space-y-4">
        <input
          type="number"
          placeholder="Número da conta"
          value={numeroConta}
          onChange={(e) => setNumeroConta(e.target.value)}
          className={inputClass}
        />
        <input
          type="number"
          placeholder="Valor"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          className={inputClass}
        />
      </div>

      <div className="grid grid-cols-2 gap-4">
        <button
          onClick={handleCriarConta}
          className={`${buttonClass} bg-green-600 hover:bg-green-700`}
        >
          Criar Conta
        </button>
        <button
          onClick={handleConsultarSaldo}
          className={`${buttonClass} bg-blue-600 hover:bg-blue-700`}
        >
          Consultar Saldo
        </button>
        <button
          onClick={handleSacar}
          className={`${buttonClass} bg-red-600 hover:bg-red-700`}
        >
          Sacar
        </button>
        <button
          onClick={handleDepositar}
          className={`${buttonClass} bg-yellow-600 hover:bg-yellow-700`}
        >
          Depositar
        </button>
      </div>

      {mensagem && (
        <div className={`${messageClass} ${mensagem.includes('Erro') ? 'bg-red-100 text-red-700' : 'bg-green-100 text-green-700'}`}>
          {mensagem}
        </div>
      )}

      {saldo !== null && (
        <div className={`${messageClass} bg-blue-100 text-blue-700`}>
          Saldo atual: R$ {saldo.toFixed(2)}
        </div>
      )}
    </div>
  );
} 