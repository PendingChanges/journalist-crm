import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Idea } from 'src/models/Idea';
import { IdeasService } from 'src/services/IdeasService';

@Component({
  selector: 'app-idea-page',
  templateUrl: './idea-page.component.html',
  styleUrls: ['./idea-page.component.scss'],
})
export class IdeaPageComponent {
  constructor(
    private _route: ActivatedRoute,
    private _ideasService: IdeasService
  ) {}

  public idea?: Observable<Idea>;

  ngOnInit(): void {
    this.idea = this._ideasService.getIdea(
      this._route.snapshot.params['id']
    );
  }
}
