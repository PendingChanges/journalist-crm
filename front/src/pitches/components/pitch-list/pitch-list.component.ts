import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Pitch, QueryAllPitchesArgs } from 'src/models/generated/graphql';
import { NgIf, NgFor, AsyncPipe } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { loading, selectPitches } from 'src/pitches/state/pitches.selectors';
import { PitchesActions } from 'src/pitches/state/pitches.actions';
import { PitchListItemComponent } from '../pitch-list-item/pitch-list-item.component';
import { LoadingRowComponent } from 'src/common/components/loading-row/loading-row.component';

@Component({
  selector: 'app-pitch-list',
  templateUrl: './pitch-list.component.html',
  styleUrls: ['./pitch-list.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    InfiniteScrollModule,
    NgIf,
    NgFor,
    PitchListItemComponent,
    AsyncPipe,
    LoadingRowComponent,
  ],
})
export class PitchListComponent {
  @Input() public showClient: boolean = false;
  @Input() public showIdea: boolean = false;
  @Input() public clientId: string | null = null;
  @Input() public ideaId: string | null = null;

  public pitches$: Observable<readonly Pitch[]> =
    this._store.select(selectPitches);

  public loading$: Observable<boolean> = this._store.select(loading);

  private skip: number = 1;

  constructor(private _store: Store) {}

  ngOnInit(): void {
    this.loadPitchList(false, this.skip);
  }

  public onScroll() {
    this.loadPitchList(true, ++this.skip);
  }

  private loadPitchList(append: boolean, skip: number) {
    this._store.dispatch(
      PitchesActions.loadPitchList({
        args: <QueryAllPitchesArgs>{
          skip: skip,
          take: 15,
          sortBy: 'name',
          clientId: this.clientId,
          ideaId: this.ideaId,
        },
        append: append,
      })
    );
  }
}
