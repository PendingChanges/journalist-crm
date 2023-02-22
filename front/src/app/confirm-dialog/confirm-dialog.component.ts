import { Component, Inject, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss'],
})
export class ConfirmDialogComponent {
  public message: string = '';
  public title: string = '';

  constructor(
    private _dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogModel
  ) {
    this.message = data.message;
    this.title = data.title;
  }

  public onConfirmClick(): void {
    this._dialogRef.close(true);
  }

  public onCancelClick(): void {
    this._dialogRef.close(false);
  }
}

export class ConfirmDialogModel {
  constructor(public title: string, public message: string) {}
}
