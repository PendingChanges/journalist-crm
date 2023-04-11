import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SaveIdeaComponent } from '../save-idea/save-idea.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-add-idea-button',
    templateUrl: './add-idea-button.component.html',
    styleUrls: ['./add-idea-button.component.scss'],
    standalone: true,
    imports: [TranslocoModule]
})
export class AddIdeaButtonComponent {
  constructor(private modalService: NgbModal) {}

  openDialog(): void {
    const dialogRef = this.modalService.open(SaveIdeaComponent);
  }
}