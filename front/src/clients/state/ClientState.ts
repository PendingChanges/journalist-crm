import { Client, Pitch } from 'src/models/generated/graphql';

export type ClientState = {
  clients: ReadonlyArray<Client>;
  errors: ReadonlyArray<string>;
  currentClient: (Client & { pitches: ReadonlyArray<Pitch> }) | null;
  loading: boolean;
};
