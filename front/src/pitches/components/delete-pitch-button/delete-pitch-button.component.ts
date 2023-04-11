import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeletePitchInput, Pitch } from 'src/models/generated/graphql';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from 'src/common/components/confirm-dialog/confirm-dialog.component';
import { PitchesActions } from 'src/pitches/state/pitches.actions';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-delete-pitch-button',
  standalone: true,
  imports: [CommonModule, TranslocoModule],
  templateUrl: './delete-pitch-button.component.html',
  styleUrls: ['./delete-pitch-button.component.scss'],
})
export class DeletePitchButtonComponent {
  @Input() public pitch: Pitch | null = null;
  @Input() public disabled = false;
  constructor(private _modalService: NgbModal, private _store: Store) {}

  openConfirmDialog(): void {
    const dialogRef = this._modalService.open(ConfirmDialogComponent);
    dialogRef.componentInstance.data = new ConfirmDialogModel(
      `Confirm ${this.pitch?.title} deletion`,
      `Are you sure you want to delete pitch ${this.pitch?.title} ?`
    );

    dialogRef.closed.subscribe((dialogResult) => {
      if (dialogResult && this.pitch) {
        this._store.dispatch(
          PitchesActions.removePitch(<DeletePitchInput>{
            id: this.pitch.id,
          })
        );
      }
    });
  }
}
