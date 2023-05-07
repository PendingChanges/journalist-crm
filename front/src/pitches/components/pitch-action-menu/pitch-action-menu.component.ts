import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pitch } from 'src/models/generated/graphql';
import { DeletePitchButtonComponent } from '../delete-pitch-button/delete-pitch-button.component';
import { PitchModifyButtonComponent } from '../pitch-modify-button/pitch-modify-button.component';

@Component({
  selector: 'app-pitch-action-menu',
  standalone: true,
  imports: [
    CommonModule,
    DeletePitchButtonComponent,
    PitchModifyButtonComponent,
  ],
  templateUrl: './pitch-action-menu.component.html',
  styleUrls: ['./pitch-action-menu.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PitchActionMenuComponent {
  @Input({ required: true }) public pitch: Pitch | null = null;
}
