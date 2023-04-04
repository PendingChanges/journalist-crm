import { Component, Inject, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-confirm-dialog',
    templateUrl: './confirm-dialog.component.html',
    styleUrls: ['./confirm-dialog.component.scss'],
    standalone: true
})
export class ConfirmDialogComponent implements OnInit {
  public message: string = '';
  public title: string = '';
  public data?: ConfirmDialogModel;
  constructor(private _activeModal: NgbActiveModal) {}
  ngOnInit(): void {
    this.message = this.data?.message || '';
    this.title = this.data?.title || '';
  }

  public onConfirmClick(): void {
    this._activeModal.close(true);
  }

  public onCancelClick(): void {
    this._activeModal.close(false);
  }
}

export class ConfirmDialogModel {
  constructor(public title: string, public message: string) {}
}
