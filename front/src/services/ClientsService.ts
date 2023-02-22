import { Injectable } from '@angular/core';
import { Observable } from '@apollo/client/core';
import { QueryRef } from 'apollo-angular';
import { map } from 'rxjs';
import { Client } from 'src/models/Client';
import { AddClientMutation } from 'src/mutations/AddClientMutation';
import { DeleteClientMutation } from 'src/mutations/DeleteClientMutation';
import { AllClientsQuery } from 'src/queries/AllClientsQuery';
import { ClientQuery } from 'src/queries/ClientQuery';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  constructor(
    private _allClientsQuery: AllClientsQuery,
    private _clientQuery: ClientQuery,
    private _addClientMutation: AddClientMutation,
    private _deleteClientMutation: DeleteClientMutation
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
      .valueChanges.pipe(map((result: any) => result.data.allClients[0]));
  }

  public addClient(value: any) {
    this._addClientMutation
      .mutate({
        name: value.name,
      })
      .subscribe(() => {
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
}
