import { Component } from '@angular/core';
import { AsyncPipe, NgIf } from '@angular/common';
import { PitchListComponent } from '../pitch-list/pitch-list.component';
import { PitchesActionMenuComponent } from '../pitches-action-menu/pitches-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-pitches-page',
  templateUrl: './pitches-page.component.html',
  styleUrls: ['./pitches-page.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    PitchesActionMenuComponent,
    PitchListComponent,
    AsyncPipe,
    NgIf,
  ],
})
export class PitchesPageComponent {}
