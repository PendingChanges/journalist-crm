import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from 'src/app/confirm-dialog/confirm-dialog.component';
import { Idea } from 'src/models/Idea';
import { IdeasService } from 'src/services/IdeasService';

@Component({
  selector: 'app-delete-idea-button',
  templateUrl: './delete-idea-button.component.html',
  styleUrls: ['./delete-idea-button.component.scss'],
})
export class DeleteIdeaButtonComponent {
  @Input() public idea: Idea | null = null;

  constructor(
    private _dialog: MatDialog,
    private _ideasService: IdeasService,
    private _router: Router
  ) {}

  openConfirmDialog(): void {
    const dialogRef = this._dialog.open(ConfirmDialogComponent, {
      width: '600px',
      data: new ConfirmDialogModel(
        `Confirm ${this.idea?.name} deletion`,
        `Are you sure you want to delete idea ${this.idea?.name} ?`
      ),
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult && this.idea) {
        this._ideasService.deleteIdea(this.idea.id);
        this._router.navigate(['/ideas']);
      }
    });
  }
}
