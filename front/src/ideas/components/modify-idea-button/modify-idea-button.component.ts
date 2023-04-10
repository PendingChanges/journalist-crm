import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Idea } from 'src/models/generated/graphql';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  SaveIdeaComponent,
  SaveIdeaModel,
} from '../save-idea/save-idea.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-modify-idea-button',
  standalone: true,
  imports: [CommonModule, TranslocoModule],
  templateUrl: './modify-idea-button.component.html',
  styleUrls: ['./modify-idea-button.component.scss'],
})
export class ModifyIdeaButtonComponent {
  @Input() public idea: Idea | null = null;

  constructor(private _modalService: NgbModal) {}
  openDialog(): void {
    const dialogRef = this._modalService.open(SaveIdeaComponent);
    dialogRef.componentInstance.data = new SaveIdeaModel('modify', this.idea);
  }
}
