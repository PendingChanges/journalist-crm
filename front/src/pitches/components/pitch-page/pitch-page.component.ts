import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Pitch } from 'src/models/generated/graphql';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { TranslocoModule } from '@ngneat/transloco';
import { NgIf, AsyncPipe, DatePipe } from '@angular/common';
import { Store } from '@ngrx/store';
import { currentPitch } from 'src/pitches/state/pitches.selectors';
import { PitchesActions } from 'src/pitches/state/pitches.actions';

@Component({
  selector: 'app-pitch-page',
  templateUrl: './pitch-page.component.html',
  styleUrls: ['./pitch-page.component.scss'],
  standalone: true,
  imports: [NgIf, TranslocoModule, RouterLink, AsyncPipe, DatePipe],
})
export class PitchPageComponent implements OnInit {
  constructor(private _route: ActivatedRoute, private _store: Store) {}
  public pitch$: Observable<Pitch | null> = this._store.select(currentPitch);

  ngOnInit(): void {
    const pitchId = this._route.snapshot.params['id'];
    this._store.dispatch(PitchesActions.loadPitch({ pitchId: pitchId }));
  }
}
