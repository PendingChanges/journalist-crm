import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pitch } from 'src/models/generated/graphql';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pitch-list-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pitch-list-item.component.html',
  styleUrls: ['./pitch-list-item.component.scss'],
})
export class PitchListItemComponent {
  constructor(private _router: Router) {}

  @Input() public pitch: Pitch | null = null;

  public onRowClick(pitch: Pitch | null) {
    if (!pitch) return;

    this._router.navigate(['/pitches', pitch.id]);
  }
}
