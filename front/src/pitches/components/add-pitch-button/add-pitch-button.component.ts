import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Client, Idea } from 'src/models/generated/graphql';

import { TranslocoModule } from '@ngneat/transloco';
import {
  AddPitchComponent,
  SavePitchDialogModel,
} from '../save-pitch/save-pitch.component';

@Component({
  selector: 'app-add-pitch-button',
  templateUrl: './add-pitch-button.component.html',
  styleUrls: ['./add-pitch-button.component.scss'],
  standalone: true,
  imports: [TranslocoModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddPitchButtonComponent {
  @Input() public client: Client | null = null;
  @Input() public idea: Idea | null = null;
  @Input() public disableClient: boolean = false;
  @Input() public disableIdea: boolean = false;

  constructor(private modalService: NgbModal) {}

  openDialog(): void {
    const dialogRef = this.modalService.open(AddPitchComponent, <
      NgbModalOptions
    >{
      size: 'lg',
    });
    dialogRef.componentInstance.data = new SavePitchDialogModel(
      'add',
      null,
      this.client,
      this.idea,
      this.disableClient,
      this.disableIdea
    );
  }
}
