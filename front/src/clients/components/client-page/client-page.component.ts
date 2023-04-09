import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import {
  Client,
  Pitch,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { PitchListComponent } from '../../../pitches/components/pitch-list/pitch-list.component';
import { ClientActionMenuComponent } from '../client-action-menu/client-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { NgIf, AsyncPipe } from '@angular/common';
import { Store } from '@ngrx/store';
import { ClientsActions } from 'src/clients/state/clients.actions';
import {
  currentClient,
  currentClientPitches,
} from 'src/clients/state/clients.selectors';

@Component({
  selector: 'app-client-page',
  templateUrl: './client-page.component.html',
  styleUrls: ['./client-page.component.scss'],
  standalone: true,
  imports: [
    NgIf,
    TranslocoModule,
    ClientActionMenuComponent,
    PitchListComponent,
    AsyncPipe,
  ],
})
export class ClientPageComponent implements OnInit {
  constructor(private _route: ActivatedRoute, private _store: Store) {}

  public client$: Observable<Client | null> = this._store.select(currentClient);
  public pitches$?: Observable<readonly Pitch[]> =
    this._store.select(currentClientPitches);

  ngOnInit(): void {
    const clientId = this._route.snapshot.params['id'];
    this._store.dispatch(ClientsActions.loadClient({ clientId: clientId }));
    this._store.dispatch(
      ClientsActions.loadClientPitchList({
        args: <QueryAllPitchesArgs>{
          skip: 0,
          take: 10,
          clientId: clientId,
          sortBy: 'name',
        },
      })
    );
  }
}
