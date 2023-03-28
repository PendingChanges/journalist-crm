import { Component, Input } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Client } from 'src/models/Client';
import { Idea } from 'src/models/Idea';
import {
  AddPitchComponent,
  AddPitchDialogModel,
} from '../add-pitch.component';

@Component({
  selector: 'app-add-pitch-button',
  templateUrl: './add-pitch-button.component.html',
  styleUrls: ['./add-pitch-button.component.scss'],
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
    dialogRef.componentInstance.data = new AddPitchDialogModel(
      this.client,
      this.idea,
      this.disableClient,
      this.disableIdea
    );
  }
}
