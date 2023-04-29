import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Idea, QueryAllIdeasArgs } from 'src/models/generated/graphql';
import { NgFor, DecimalPipe, AsyncPipe, NgIf } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { Observable } from 'rxjs';
import { loading, selectIdeas } from 'src/ideas/state/ideas.selectors';
import { Store } from '@ngrx/store';
import { IdeasActions } from 'src/ideas/state/ideas.actions';
import { IdeaListItemComponent } from '../idea-list-item/idea-list-item.component';
import { LoadingRowComponent } from 'src/common/components/loading-row/loading-row.component';

@Component({
  selector: 'app-idea-list',
  templateUrl: './idea-list.component.html',
  styleUrls: ['./idea-list.component.scss'],
  standalone: true,
  imports: [
    InfiniteScrollModule,
    NgFor,
    NgIf,
    DecimalPipe,
    IdeaListItemComponent,
    AsyncPipe,
    LoadingRowComponent
  ],
})
export class IdeaListComponent {
  public ideas$: Observable<readonly Idea[]> = this._store.select(selectIdeas);

  public loading$: Observable<boolean> = this._store.select(loading);

  private skip: number = 1;

  constructor(private _store: Store) {}

  ngOnInit(): void {
    this.loadIdeaList(false, this.skip);
  }

  public onScroll() {
    this.loadIdeaList(true, ++this.skip);
  }

  private loadIdeaList(append: boolean, skip: number) {
    this._store.dispatch(
      IdeasActions.loadIdeaList({
        args: <QueryAllIdeasArgs>{
          skip: skip,
          take: 15,
          sortBy: 'name',
        },
        append: append,
      })
    );
  }
}
