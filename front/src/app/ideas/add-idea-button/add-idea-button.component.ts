import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddIdeaComponent } from '../add-idea/add-idea.component';

@Component({
  selector: 'app-add-idea-button',
  templateUrl: './add-idea-button.component.html',
  styleUrls: ['./add-idea-button.component.scss'],
})
export class AddIdeaButtonComponent {
  constructor(public dialog: MatDialog) {}

  openDialog(): void {
    const dialogRef = this.dialog.open(AddIdeaComponent, {
      width: '600px',
    });
  }
}
