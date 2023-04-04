import { Injectable } from '@angular/core';
import { QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import {
  AddClientMutation,
  ClientInput,
} from 'src/mutations/AddClientMutation';
import { DeleteClientMutation } from 'src/mutations/DeleteClientMutation';
import {
  RenameClientInput,
  RenameClientMutation,
} from 'src/mutations/RenameClientMutation';
import { AllClientsQuery } from 'src/queries/AllClientsQuery';
import { AutoCompleteClientQuery } from 'src/queries/AutoCompleteClientQuery';
import { ClientQuery } from 'src/queries/ClientQuery';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  constructor(
    private _allClientsQuery: AllClientsQuery,
    private _clientQuery: ClientQuery,
    private _addClientMutation: AddClientMutation,
    private _deleteClientMutation: DeleteClientMutation,
    private _autoCompleteClientQuery: AutoCompleteClientQuery,
    private _renameClientMutation: RenameClientMutation
  ) {}

  private _allClientsQueryRef: QueryRef<any> | null = null;

  public get clients$() {
    if (!this._allClientsQueryRef) {
      this._allClientsQueryRef = this._allClientsQuery.watch();
    }

    return this._allClientsQueryRef.valueChanges.pipe(
      map((result: any) => result.data.allClients.items)
    );
  }

  public refreshClients(): void {
    this._allClientsQueryRef?.refetch();
  }

  public getClient(id: string) {
    return this._clientQuery
      .watch({
        id: id,
      })
      .valueChanges.pipe(map((result: any) => result.data.client));
  }

  public addClient(value: ClientInput) {
    this._addClientMutation.mutate(value).subscribe(() => {
      this.refreshClients();
    });
  }

  modifyClient(value: RenameClientInput) {
    this._renameClientMutation.mutate(value).subscribe(() => {
      this.refreshClients();
    });
  }

  public deleteClient(id: string) {
    this._deleteClientMutation
      .mutate({
        id: id,
      })
      .subscribe(() => {
        this.refreshClients();
      });
  }

  public autoComplete(text: string): Observable<Client[]> {
    return this._autoCompleteClientQuery
      .fetch({ text: text })
      .pipe(map((result) => result.data.autoCompleteClient));
  }

  public assertTruc(obj: any): obj is String {
    return obj instanceof String;
  }
}
