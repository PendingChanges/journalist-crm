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
} from 'src/models/generated/graphql';
import { AddClientMutation } from 'src/clients/mutations/AddClientMutation';
import { CreateClientInput } from 'src/models/generated/graphql';
import { AllClientsQuery } from 'src/clients/queries/AllClientsQuery';
import { AutoCompleteClientQuery } from 'src/clients/queries/AutoCompleteClientQuery';
import { ApolloQueryResult } from '@apollo/client/core';
import { ClientQuery } from '../queries/ClientQuery';
import { DeleteClientMutation } from '../mutations/DeleteClientMutation';
import { RenameClientMutation } from '../mutations/RenameClientMutation';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  private _allClientsQueryRef: QueryRef<
    { allClients: AllClientsCollectionSegment },
    QueryAllClientsArgs
  > | null = null;

  constructor(
    private _allClientsQuery: AllClientsQuery,
    private _clientQuery: ClientQuery,
    private _addClientMutation: AddClientMutation,
    private _deleteClientMutation: DeleteClientMutation,
    private _autoCompleteClientQuery: AutoCompleteClientQuery,
    private _renameClientMutation: RenameClientMutation
  ) {}

  public getClients(args: QueryAllClientsArgs) {
    return this._allClientsQuery.fetch(args);
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
