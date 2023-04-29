import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Client, QueryAllClientsArgs } from 'src/models/generated/graphql';
import { NgFor, DecimalPipe, AsyncPipe, NgIf } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loading, selectClients } from 'src/clients/state/clients.selectors';
import { ClientsActions } from 'src/clients/state/clients.actions';
import { ClientListItemComponent } from '../client-list-item/client-list-item.component';
import { LoadingRowComponent } from 'src/common/components/loading-row/loading-row.component';

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
    ClientListItemComponent,
    LoadingRowComponent,
    NgIf,
  ],
})
export class ClientListComponent {
  public clients$: Observable<readonly Client[]> =
    this._store.select(selectClients);

  public loading$: Observable<boolean> = this._store.select(loading);

  private skip: number = 1;

  constructor(private _store: Store) {}

  ngOnInit(): void {
    this.loadClientList(false, this.skip);
  }

  public onScroll() {
    this.loadClientList(true, ++this.skip);
  }

  private loadClientList(append: boolean, skip: number) {
    this._store.dispatch(
      ClientsActions.loadClientList({
        args: <QueryAllClientsArgs>{
          skip: skip,
          take: 15,
          sortBy: 'name',
        },
        append: append,
      })
    );
  }
}
