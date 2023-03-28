import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from 'src/app/confirm-dialog/confirm-dialog.component';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';

@Component({
  selector: 'app-client-delete-button',
  templateUrl: './client-delete-button.component.html',
  styleUrls: ['./client-delete-button.component.scss'],
})
export class ClientDeleteButtonComponent {
  @Input() public client: Client | null = null;
  @Input() public disabled = false;
  constructor(
    private _modalService: NgbModal,
    private _clientsService: ClientsService,
    private _router: Router
  ) {}

  openConfirmDialog(): void {
    const dialogRef = this._modalService.open(ConfirmDialogComponent);
    dialogRef.componentInstance.data = new ConfirmDialogModel(
      `Confirm ${this.client?.name} deletion`,
      `Are you sure you want to delete client ${this.client?.name} ?`
    );

    dialogRef.closed.subscribe((dialogResult) => {
      if (dialogResult && this.client) {
        this._clientsService.deleteClient(this.client.id);
        this._router.navigate(['/clients']);
      }
    });
  }
}
