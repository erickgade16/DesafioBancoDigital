# API de Banco Digital

Uma API bancária baseada em GraphQL construída com .NET 9, utilizando Supabase para armazenamento de dados e hospedada no Render.

## API em Produção

A API está disponível em:
[https://banco-digital-api.onrender.com/graphql/](https://banco-digital-api.onrender.com/graphql/)

## Arquitetura

- **API**: GraphQL com HotChocolate
- **Banco de Dados**: PostgreSQL (Supabase)
- **Hospedagem**: Render
- **Framework**: .NET 9
- **Padrão de Arquitetura**: Clean Architecture

## Stack Tecnológica

- Entity Framework Core
- HotChocolate GraphQL
- xUnit para Testes
- Moq para Simulação de Objetos
- Banco de Dados Em-Memória para Testes

## Funcionalidades

- Criação de Conta
- Consulta de Saldo
- Depósitos
- Saques
- Tratamento de Erros
- Testes Automatizados

## Operações GraphQL

### Query

```graphql
query {
  saldo(conta: 1)
}
```

### Mutation

1. Criar Conta
```graphql
mutation {
  criarConta(saldoInicial: 1000.0) {
    numeroConta
    saldoConta
  }
}
```

2. Depositar
```graphql
mutation {
  depositar(conta: 1, valor: 500.0) {
    numeroConta
    saldoConta
  }
}
```

3. Sacar
```graphql
mutation {
  sacar(conta: 1, valor: 200.0) {
    numeroConta
    saldoConta
  }
}
```

## Testes

O projeto inclui testes em todas as camadas:

- Testes de Domínio
- Testes de Serviços
- Testes de Repositório

Para executar os testes:
```bash
dotnet test
```


## Variáveis de Ambiente

As seguintes variáveis de ambiente são necessárias:

- `ConnectionStrings__DefaultConnection`: String de conexão do PostgreSQL do Supabase

## Tratamento de Erros

A API retorna os seguintes códigos de erro:

- `Conta nao encontrada`: Quando a conta não existe no sistema
- `Valor invalido`: Quando o valor da operação é inválido
- `Sem saldo`: Quando não há saldo suficiente para a operação
