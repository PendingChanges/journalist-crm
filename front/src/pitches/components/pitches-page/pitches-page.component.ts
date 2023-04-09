import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pitch, QueryAllPitchesArgs } from 'src/models/generated/graphql';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { AsyncPipe, NgIf } from '@angular/common';
import { PitchListComponent } from '../pitch-list/pitch-list.component';
import { PitchesActionMenuComponent } from '../pitches-action-menu/pitches-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { PitchesActions } from 'src/pitches/state/pitches.actions';
import { loading, selectPitches } from 'src/pitches/state/pitches.selectors';

@Component({
  selector: 'app-pitches-page',
  templateUrl: './pitches-page.component.html',
  styleUrls: ['./pitches-page.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    PitchesActionMenuComponent,
    PitchListComponent,
    AsyncPipe,
    NgIf,
  ],
})
export class PitchesPageComponent implements OnInit {
  constructor(private _store: Store) {}
  ngOnInit(): void {
    this._store.dispatch(
      PitchesActions.loadPitchList({
        args: <QueryAllPitchesArgs>{
          skip: 0,
          take: 10,
          sortBy: 'name',
        },
      })
    );
  }

  public pitches$: Observable<readonly Pitch[]> =
    this._store.select(selectPitches);

  public loading$: Observable<boolean> = this._store.select(loading);
}
