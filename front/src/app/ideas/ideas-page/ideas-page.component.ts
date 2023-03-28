import { Component, OnInit } from '@angular/core';
import { Idea } from 'src/models/Idea';
import { Observable } from 'rxjs';
import { IdeasService } from 'src/services/IdeasService';

@Component({
  selector: 'app-ideas-page',
  templateUrl: './ideas-page.component.html',
  styleUrls: ['./ideas-page.component.scss'],
})
export class IdeasComponent implements OnInit {
  public ideas$?: Observable<Idea[]>;

  constructor(private _ideasService: IdeasService) {}

  ngOnInit(): void {
    this.ideas$ = this._ideasService.ideas$;
  }
}
