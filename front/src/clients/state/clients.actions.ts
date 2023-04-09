import { createActionGroup, props } from '@ngrx/store';
import {
  Client,
  ClientAddedPayload,
  CreateClientInput,
  DeleteClientInput,
  QueryAllClientsArgs,
  RenameClientInput,
} from 'src/models/generated/graphql';
import { ErrorProps } from '../../common/state/ErrorProps';

export const ClientsActions = createActionGroup({
  source: 'Clients',
  events: {
    'Add Client': props<CreateClientInput>(),
    'Remove Client': props<DeleteClientInput>(),
    'Rename Client': props<RenameClientInput>(),
    'Load Client': props<{ clientId: string }>(),
    'Load Client List': props<{ args: QueryAllClientsArgs; date?: Date }>(),
    'Client List Loaded Success': props<{ clients: ReadonlyArray<Client> }>(),
    'Client List Loaded Failure': props<ErrorProps>(),
    'Client Loaded Success': props<{ client: Client }>(),
    'Client Loaded Failure': props<ErrorProps>(),
    'Client Added Success': props<{
      payload: ClientAddedPayload;
      args: QueryAllClientsArgs;
      date?: Date;
    }>(),
    'Client Added Failure': props<ErrorProps>(),
    'Client Removed Success': props<{ payload: string }>(),
    'Client Removed Failure': props<ErrorProps>(),
    'Client Renamed Success': props<{ payload: string; newName: string }>(),
    'Client Renamed Failure': props<ErrorProps>(),
  },
});