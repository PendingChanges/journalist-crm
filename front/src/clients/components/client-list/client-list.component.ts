import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Client, QueryAllClientsArgs } from 'src/models/generated/graphql';
import { NgFor, DecimalPipe, AsyncPipe } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loading, selectClients } from 'src/clients/state/clients.selectors';
import { ClientsActions } from 'src/clients/state/clients.actions';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    InfiniteScrollModule,
    AsyncPipe,
    NgFor,
    DecimalPipe,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientListComponent {
  @Input() public clients: readonly Client[] | null = [];

  public clients$: Observable<readonly Client[]> =
    this._store.select(selectClients);

  public loading$: Observable<boolean> = this._store.select(loading);

  constructor(private _router: Router, private _store: Store) {}
  public onRowClick(client: Client) {
    this._router.navigate(['/clients', client.id]);
  }

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

  public onScroll() {}
}
