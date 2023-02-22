import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddClientComponent } from '../add-client/add-client.component';

@Component({
  selector: 'app-add-client-button',
  templateUrl: './add-client-button.component.html',
  styleUrls: ['./add-client-button.component.scss'],
})
export class AddClientButtonComponent {
  constructor(public dialog: MatDialog) {}
  openDialog(): void {
    this.dialog.open(AddClientComponent, {
      width: '600px',
    });
  }
}
