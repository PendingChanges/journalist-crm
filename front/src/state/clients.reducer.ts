import { createReducer, on } from '@ngrx/store';
import {
  ClientsActions,
  ClientsApiActions,
  CLientsPageActions,
} from './clients.actions';
import { ClientState } from './ClientState';

export const initialState: ClientState = {
  clients: [],
  errors: [],
  currentClient: null,
  loading: false,
};

export const clientsReducer = createReducer(
  initialState,
  on(CLientsPageActions.clientsPageOpened, (state, _a) => {
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
  on(ClientsApiActions.clientListLoadedSuccess, (state, result) => {
    return <ClientState>{
      ...state,
      clients: result.clients,
      errors: [],
      loading: false,
    };
  }),
  on(ClientsApiActions.clientListLoadedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      clients: [],
      errors: result.errors,
      loading: false,
    };
  }),
  on(ClientsApiActions.clientLoadedSuccess, (state, result) => {
    return <ClientState>{
      ...state,
      currentClient: result.client,
      loading: false,
    };
  }),
  on(ClientsApiActions.clientLoadedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      currentClient: null,
      errors: result.errors,
      loading: false,
    };
  }),
  on(ClientsApiActions.clientRenamedSuccess, (state, renameClient) => {
    return <ClientState>{
      ...state,
      currentClient: {
        ...state.currentClient,
        name: renameClient.newName,
      },
      loading: false,
    };
  }),
  on(ClientsApiActions.clientRenamedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  }),
  on(ClientsApiActions.clientRemovedSuccess, (state, removeClient) => {
    return <ClientState>{
      ...state,
      currentClient: null,
      loading: false,
    };
  }),
  on(ClientsApiActions.clientRemovedFailure, (state, result) => {
    return <ClientState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  })
);
