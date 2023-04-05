import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Pitch } from 'src/generated/graphql';
import { PitchesService } from 'src/services/PitchesService';
import { AsyncPipe } from '@angular/common';
import { PitchListComponent } from '../pitch-list/pitch-list.component';
import { PitchesActionMenuComponent } from '../pitches-action-menu/pitches-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-pitches-page',
    templateUrl: './pitches-page.component.html',
    styleUrls: ['./pitches-page.component.scss'],
    standalone: true,
    imports: [TranslocoModule, PitchesActionMenuComponent, PitchListComponent, AsyncPipe]
})
export class PitchesPageComponent implements OnInit{
  public pitches$?: Observable<Pitch[]>;

  constructor(private _pitchesService: PitchesService) {}

  ngOnInit(): void {
    this.pitches$ = this._pitchesService.pitches$;
  }
}
