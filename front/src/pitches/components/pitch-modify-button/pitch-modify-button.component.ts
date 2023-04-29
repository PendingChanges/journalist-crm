import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pitch } from 'src/models/generated/graphql';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  AddPitchComponent,
  SavePitchDialogModel,
} from '../save-pitch/save-pitch.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-pitch-modify-button',
  standalone: true,
  imports: [CommonModule, TranslocoModule],
  templateUrl: './pitch-modify-button.component.html',
  styleUrls: ['./pitch-modify-button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PitchModifyButtonComponent {
  @Input() public pitch: Pitch | null = null;
  constructor(private _modalService: NgbModal) {}
  openDialog(): void {
    const dialogRef = this._modalService.open(AddPitchComponent);
    dialogRef.componentInstance.data = new SavePitchDialogModel(
      'modify',
      this.pitch,
      null,
      null,
      false,
      false
    );
  }
}
