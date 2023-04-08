import { inject } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { MutationResult } from 'apollo-angular';
import { catchError, switchMap, map, of, tap } from 'rxjs';
import {
  AllClientsCollectionSegment,
  Client,
  ClientAddedPayload,
  QueryAllClientsArgs,
} from 'src/generated/graphql';
import { ClientsService } from 'src/services/ClientsService';
import {
  ClientsActions,
  ClientsApiActions,
  CLientsPageActions,
} from './clients.actions';
import { Router } from '@angular/router';

export const loadClients = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(CLientsPageActions.clientsPageOpened),
      switchMap((a: { args: QueryAllClientsArgs; date?: Date }) => {
        clientsService.refreshClients(a.args);
        return clientsService.clientListResult$.pipe(
          map((clientListResult) =>
            ClientsApiActions.clientListLoadedSuccess({
              clients: clientListResult.data.allClients.items || [],
            })
          ),
          catchError((result: ApolloQueryResult<AllClientsCollectionSegment>) =>
            of(
              ClientsApiActions.clientListLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const loadClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(CLientsPageActions.clientPageOpened),
      switchMap(({ clientId }) => {
        return clientsService.getClient(clientId).pipe(
          map((clientResult) =>
            ClientsApiActions.clientLoadedSuccess({
              client: clientResult.data.client,
            })
          ),
          catchError((result: ApolloQueryResult<{ client: Client }>) =>
            of(
              ClientsApiActions.clientLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const addClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.addClient),
      switchMap((addClient) => {
        return clientsService.addClient(addClient).pipe(
          map((addClientResult) =>
            ClientsApiActions.clientAddedSuccess({
              payload: <ClientAddedPayload>addClientResult.data?.addClient,
              args: <QueryAllClientsArgs>{
                skip: 0,
                sortBy: 'name',
                take: 10,
              },
              date: new Date(),
            })
          ),
          catchError(
            (result: MutationResult<{ addClient: ClientAddedPayload }>) =>
              of(
                ClientsApiActions.clientAddedFailure({
                  errors: result.errors?.map((e) => e.message) || [
                    'Unknown error',
                  ],
                })
              )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const renameClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.renameClient),
      switchMap((renameClient) => {
        return clientsService.renameClient(renameClient).pipe(
          map((renameClientResult) =>
            ClientsApiActions.clientRenamedSuccess({
              payload: <string>renameClientResult.data,
              newName: renameClient.newName,
            })
          ),
          catchError((result: MutationResult<string>) =>
            of(
              ClientsApiActions.clientRenamedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const removeClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.removeClient),
      switchMap((removeClient) => {
        return clientsService.removeClient(removeClient).pipe(
          map((removeClientResult) =>
            ClientsApiActions.clientRemovedSuccess({
              payload: <string>removeClientResult.data?.removeClient,
            })
          ),
          catchError((result: MutationResult<{ removeClient: string }>) =>
            of(
              ClientsApiActions.clientRemovedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const redirectToClient = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(ClientsApiActions.clientAddedSuccess),
      tap((result) => {
        router.navigate(['/clients', result.payload.clientId]);
      })
    );
  },
  { functional: true, dispatch: false }
);

export const redirectToClients = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(ClientsApiActions.clientRemovedSuccess),
      tap(() => {
        router.navigate(['/clients']);
      })
    );
  },
  { functional: true, dispatch: false }
);
