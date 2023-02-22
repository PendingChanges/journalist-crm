import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Client } from 'src/models/Client';
import { Idea } from 'src/models/Idea';
import {
  AddPitchComponent,
  AddPitchDialogModel,
} from '../add-pitch/add-pitch.component';

@Component({
  selector: 'app-add-pitch-button',
  templateUrl: './add-pitch-button.component.html',
  styleUrls: ['./add-pitch-button.component.scss'],
})
export class AddPitchButtonComponent {
  @Input() public client?: Client;
  @Input() public idea?: Idea;

  constructor(public dialog: MatDialog) {}

  openDialog(): void {
    const dialogRef = this.dialog.open(AddPitchComponent, {
      width: '1000px',
      data: new AddPitchDialogModel(this.client, this.idea),
    });
  }
}
