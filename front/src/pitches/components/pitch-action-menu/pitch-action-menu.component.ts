import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pitch } from 'src/models/generated/graphql';
import { DeletePitchButtonComponent } from '../delete-pitch-button/delete-pitch-button.component';

@Component({
  selector: 'app-pitch-action-menu',
  standalone: true,
  imports: [CommonModule, DeletePitchButtonComponent],
  templateUrl: './pitch-action-menu.component.html',
  styleUrls: ['./pitch-action-menu.component.scss'],
})
export class PitchActionMenuComponent {
  @Input() public pitch: Pitch | null = null;
}
