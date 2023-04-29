import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AddPitchButtonComponent } from '../add-pitch-button/add-pitch-button.component';

@Component({
  selector: 'app-pitches-action-menu',
  templateUrl: './pitches-action-menu.component.html',
  styleUrls: ['./pitches-action-menu.component.scss'],
  standalone: true,
  imports: [AddPitchButtonComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PitchesActionMenuComponent {}
