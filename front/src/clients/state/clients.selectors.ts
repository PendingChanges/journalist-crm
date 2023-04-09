import { createSelector, createFeatureSelector } from '@ngrx/store';
import { ClientState } from './ClientState';

export const selectClientState =
  createFeatureSelector<ClientState>('clientState');

export const selectClients = createSelector(
  selectClientState,
  (clientState: ClientState) => {
    return clientState.clients;
  }
);

export const currentClient = createSelector(
  selectClientState,
  (clientState: ClientState) => {
    return clientState.currentClient;
  }
);

export const currentClientPitches = createSelector(currentClient, (client) => {
  return client?.pitches || [];
});

export const loading = createSelector(
  selectClientState,
  (clientState: ClientState) => {
    return clientState.loading;
  }
);
