import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Pitch, Idea, QueryAllPitchesArgs } from 'src/models/generated/graphql';
import { IdeasService } from 'src/ideas/services/IdeasService';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { PitchListComponent } from '../../../pitches/components/pitch-list/pitch-list.component';
import { IdeaActionMenuComponent } from '../idea-action-menu/idea-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { NgIf, AsyncPipe } from '@angular/common';
import { Store } from '@ngrx/store';
import { IdeasActions } from 'src/ideas/state/ideas.actions';
import {
  currentIdea,
  currentIdeaPitches,
} from 'src/ideas/state/ideas.selectors';

@Component({
  selector: 'app-idea-page',
  templateUrl: './idea-page.component.html',
  styleUrls: ['./idea-page.component.scss'],
  standalone: true,
  imports: [
    NgIf,
    TranslocoModule,
    IdeaActionMenuComponent,
    PitchListComponent,
    AsyncPipe,
  ],
})
export class IdeaPageComponent {
  constructor(private _route: ActivatedRoute, private _store: Store) {}

  public idea$: Observable<Idea | null> = this._store.select(currentIdea);
  public pitches$?: Observable<readonly Pitch[]> =
    this._store.select(currentIdeaPitches);

  ngOnInit(): void {
    const ideaId = this._route.snapshot.params['id'];

    this._store.dispatch(IdeasActions.loadIdea({ ideaId: ideaId }));
    this._store.dispatch(
      IdeasActions.loadIdeaPitchList({
        args: <QueryAllPitchesArgs>{
          skip: 0,
          take: 10,
          ideaId: ideaId,
          sortBy: 'name',
        },
      })
    );
  }
}
