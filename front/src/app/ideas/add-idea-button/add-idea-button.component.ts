import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddIdeaComponent } from '../add-idea/add-idea.component';

@Component({
  selector: 'app-add-idea-button',
  templateUrl: './add-idea-button.component.html',
  styleUrls: ['./add-idea-button.component.scss'],
})
export class AddIdeaButtonComponent {
  constructor(private modalService: NgbModal) {}

  openDialog(): void {
    const dialogRef = this.modalService.open(AddIdeaComponent);
  }
}
