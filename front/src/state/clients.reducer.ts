import { createReducer, on } from '@ngrx/store';
import { ClientsActions } from './clients.actions';
import { ClientState } from './ClientState';

export const initialState: ClientState = {
  clients: [],
  errors: [],
  currentClient: null,
  loading: false,
};

export const clientsReducer = createReducer(
  initialState,
  on(ClientsActions.loadClientList, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(ClientsActions.addClient, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(ClientsActions.renameClient, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(ClientsActions.removeClient, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(ClientsActions.clientListLoadedSuccess, (state, result) => {
    return <ClientState>{
      ...state,
      clients: result.clients,
      errors: [],
      loading: false,
    };
  }),
  on(ClientsActions.clientListLoadedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      clients: [],
      errors: result.errors,
      loading: false,
    };
  }),
  on(ClientsActions.clientLoadedSuccess, (state, result) => {
    return <ClientState>{
      ...state,
      currentClient: result.client,
      loading: false,
    };
  }),
  on(ClientsActions.clientLoadedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      currentClient: null,
      errors: result.errors,
      loading: false,
    };
  }),
  on(ClientsActions.clientRenamedSuccess, (state, renameClient) => {
    return <ClientState>{
      ...state,
      currentClient: {
        ...state.currentClient,
        name: renameClient.newName,
      },
      loading: false,
    };
  }),
  on(ClientsActions.clientRenamedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  }),
  on(ClientsActions.clientRemovedSuccess, (state, removeClient) => {
    return <ClientState>{
      ...state,
      currentClient: null,
      loading: false,
    };
  }),
  on(ClientsActions.clientRemovedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  })
);
