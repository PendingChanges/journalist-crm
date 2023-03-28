import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddClientComponent } from '../add-client/add-client.component';

@Component({
  selector: 'app-add-client-button',
  templateUrl: './add-client-button.component.html',
  styleUrls: ['./add-client-button.component.scss'],
})
export class AddClientButtonComponent {
  constructor(private _modalService: NgbModal) {}
  openDialog(): void {
    this._modalService.open(AddClientComponent);
  }
}
