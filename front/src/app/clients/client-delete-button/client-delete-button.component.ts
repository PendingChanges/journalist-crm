import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
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
  @Input() public client?: Client;

  constructor(
    private _dialog: MatDialog,
    private _clientsService: ClientsService,
    private _router: Router
  ) {}

  openConfirmDialog(): void {
    const dialogRef = this._dialog.open(ConfirmDialogComponent, {
      width: '600px',
      data: new ConfirmDialogModel(
        `Confirm ${this.client?.name} deletion`,
        `Are you sure you want to delete client ${this.client?.name} ?`
      ),
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult && this.client) {
        this._clientsService.deleteClient(this.client.id);
        this._router.navigate(['/clients']);
      }
    });
  }
}
