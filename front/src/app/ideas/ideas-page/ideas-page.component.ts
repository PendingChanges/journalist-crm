import { Component, OnInit } from '@angular/core';
import { Idea, QueryAllIdeasArgs } from 'src/generated/graphql';
import { Observable } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import { IdeaListComponent } from '../idea-list/idea-list.component';
import { IdeasActionMenuComponent } from '../ideas-action-menu/ideas-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { IdeasActions } from 'src/state/ideas.actions';
import { loading, selectIdeas } from 'src/state/ideas.selectors';

@Component({
  selector: 'app-ideas-page',
  templateUrl: './ideas-page.component.html',
  styleUrls: ['./ideas-page.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TranslocoModule,
    IdeasActionMenuComponent,
    IdeaListComponent,
    AsyncPipe,
  ],
})
export class IdeasComponent implements OnInit {
  public ideas$: Observable<readonly Idea[]> = this._store.select(selectIdeas);

  public loading$: Observable<boolean> = this._store.select(loading);
  constructor(private _store: Store) {}

  ngOnInit(): void {
    this._store.dispatch(
      IdeasActions.loadIdeaList({
        args: <QueryAllIdeasArgs>{
          skip: 0,
          take: 10,
          sortBy: 'name',
        },
      })
    );
  }
}
