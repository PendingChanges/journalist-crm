import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from 'src/app/common/confirm-dialog/confirm-dialog.component';
import { DeleteIdeaInput, Idea } from 'src/generated/graphql';
import { IdeasService } from 'src/services/IdeasService';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { IdeasActions } from 'src/state/ideas.actions';

@Component({
  selector: 'app-delete-idea-button',
  templateUrl: './delete-idea-button.component.html',
  styleUrls: ['./delete-idea-button.component.scss'],
  standalone: true,
  imports: [TranslocoModule],
})
export class DeleteIdeaButtonComponent {
  @Input() public idea: Idea | null = null;
  @Input() public disabled = false;
  constructor(private _modalService: NgbModal, private _store: Store) {}

  openConfirmDialog(): void {
    const dialogRef = this._modalService.open(ConfirmDialogComponent);
    dialogRef.componentInstance.data = new ConfirmDialogModel(
      `Confirm ${this.idea?.name} deletion`,
      `Are you sure you want to delete idea ${this.idea?.name} ?`
    );

    dialogRef.closed.subscribe((dialogResult) => {
      if (dialogResult && this.idea) {
        this._store.dispatch(
          IdeasActions.removeIdea(<DeleteIdeaInput>{
            id: this.idea.id,
          })
        );
      }
    });
  }
}
