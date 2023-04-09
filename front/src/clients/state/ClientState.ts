import { Client } from 'src/models/generated/graphql';

export type ClientState = {
  clients: ReadonlyArray<Client>;
  errors: ReadonlyArray<string>;
  currentClient: Client | null;
  loading: boolean;
};
