import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Client, QueryAllClientsArgs } from 'src/models/generated/graphql';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ClientListComponent } from '../client-list/client-list.component';
import { ClientsActionMenuComponent } from '../clients-action-menu/clients-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { loading, selectClients } from 'src/clients/state/clients.selectors';
import { ClientsActions } from 'src/clients/state/clients.actions';

@Component({
  selector: 'app-clients-page',
  templateUrl: './clients-page.component.html',
  styleUrls: ['./clients-page.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    CommonModule,
    ClientsActionMenuComponent,
    ClientListComponent,
    AsyncPipe,
  ],
})
export class ClientsComponent implements OnInit {
  constructor(private _store: Store) {}
  ngOnInit(): void {
    this._store.dispatch(
      ClientsActions.loadClientList({
        args: <QueryAllClientsArgs>{
          skip: 0,
          take: 10,
          sortBy: 'name',
        },
      })
    );
  }

  public clients$: Observable<readonly Client[]> =
    this._store.select(selectClients);

  public loading$: Observable<boolean> = this._store.select(loading);
}