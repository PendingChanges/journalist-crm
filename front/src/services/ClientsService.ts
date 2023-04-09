import { Injectable } from '@angular/core';
import { MutationResult, QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import {
  AllClientsCollectionSegment,
  Client,
  ClientAddedPayload,
  DeleteClientInput,
  QueryAllClientsArgs,
  QueryAutoCompleteClientArgs,
  RenameClientInput,
} from 'src/generated/graphql';
import { AddClientMutation } from 'src/mutations/AddClientMutation';
import { CreateClientInput } from 'src/generated/graphql';
import { DeleteClientMutation } from 'src/mutations/DeleteClientMutation';
import { RenameClientMutation } from 'src/mutations/RenameClientMutation';
import { AllClientsQuery } from 'src/queries/AllClientsQuery';
import { AutoCompleteClientQuery } from 'src/queries/AutoCompleteClientQuery';
import { ClientQuery } from 'src/queries/ClientQuery';
import { ApolloQueryResult } from '@apollo/client/core';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  private _allClientsQueryRef: QueryRef<
    { allClients: AllClientsCollectionSegment },
    QueryAllClientsArgs
  > | null = null;

  constructor(
    allClientsQuery: AllClientsQuery,
    private _clientQuery: ClientQuery,
    private _addClientMutation: AddClientMutation,
    private _deleteClientMutation: DeleteClientMutation,
    private _autoCompleteClientQuery: AutoCompleteClientQuery,
    private _renameClientMutation: RenameClientMutation
  ) {
    this._allClientsQueryRef = allClientsQuery.watch();
    this.clientListResult$ = this._allClientsQueryRef.valueChanges;
  }

  public clientListResult$: Observable<
    ApolloQueryResult<{ allClients: AllClientsCollectionSegment }>
  >;

  public refreshClients(args: QueryAllClientsArgs): void {
    this._allClientsQueryRef?.refetch(args);
  }

  public getClient(id: string) {
    return this._clientQuery.watch({
      id: id,
    }).valueChanges;
  }

  public addClient(
    value: CreateClientInput
  ): Observable<MutationResult<{ addClient: ClientAddedPayload }>> {
    return this._addClientMutation.mutate(value);
  }

  public renameClient(
    value: RenameClientInput
  ): Observable<MutationResult<string>> {
    return this._renameClientMutation.mutate(value);
  }

  public removeClient(
    deleteClientInput: DeleteClientInput
  ): Observable<MutationResult<{ removeClient: string }>> {
    return this._deleteClientMutation.mutate(deleteClientInput);
  }

  public autoComplete(text: string): Observable<Client[]> {
    return this._autoCompleteClientQuery
      .fetch(<QueryAutoCompleteClientArgs>{ text: text })
      .pipe(map((result) => result.data.autoCompleteClient));
  }
}
