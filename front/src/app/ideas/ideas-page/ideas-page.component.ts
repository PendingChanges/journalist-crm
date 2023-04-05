import { Component, OnInit } from '@angular/core';
import { Idea } from 'src/generated/graphql';
import { Observable } from 'rxjs';
import { IdeasService } from 'src/services/IdeasService';
import { AsyncPipe } from '@angular/common';
import { IdeaListComponent } from '../idea-list/idea-list.component';
import { IdeasActionMenuComponent } from '../ideas-action-menu/ideas-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-ideas-page',
  templateUrl: './ideas-page.component.html',
  styleUrls: ['./ideas-page.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    IdeasActionMenuComponent,
    IdeaListComponent,
    AsyncPipe,
  ],
})
export class IdeasComponent implements OnInit {
  public ideas$?: Observable<Idea[]>;

  constructor(private _ideasService: IdeasService) {}

  ngOnInit(): void {
    this.ideas$ = this._ideasService.ideas$;
  }
}
