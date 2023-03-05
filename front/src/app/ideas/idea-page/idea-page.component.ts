import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Idea } from 'src/models/Idea';
import { Pitch } from 'src/models/Pitch';
import { IdeasService } from 'src/services/IdeasService';
import { PitchesService } from 'src/services/PitchesService';

@Component({
  selector: 'app-idea-page',
  templateUrl: './idea-page.component.html',
  styleUrls: ['./idea-page.component.scss'],
})
export class IdeaPageComponent {
  constructor(
    private _route: ActivatedRoute,
    private _ideasService: IdeasService,
    private _pitchesService: PitchesService
  ) {}

  public idea?: Observable<Idea>;
  public pitches$?: Observable<Pitch[]>;

  ngOnInit(): void {
    const ideaId = this._route.snapshot.params['id'];

    this.idea = this._ideasService.getIdea(ideaId);
    this.pitches$ = this._pitchesService.pitchesByIdeaId$(ideaId);
  }
}
