import { Component, OnInit } from '@angular/core';
import { Idea, QueryAllIdeasArgs } from 'src/models/generated/graphql';
import { Observable } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import { IdeaListComponent } from '../idea-list/idea-list.component';
import { IdeasActionMenuComponent } from '../ideas-action-menu/ideas-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { IdeasActions } from 'src/ideas/state/ideas.actions';
import { loading, selectIdeas } from 'src/ideas/state/ideas.selectors';

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
export class IdeasComponent {}
