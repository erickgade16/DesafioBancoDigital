import { ApolloClient, InMemoryCache, createHttpLink } from '@apollo/client';

const httpLink = createHttpLink({
  uri: 'https://localhost:7176/graphql/', // URL atualizada
});

const client = new ApolloClient({
  link: httpLink,
  cache: new InMemoryCache(),
});

export default client; 