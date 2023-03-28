import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {
  ClientInput,
} from 'src/mutations/AddClientMutation';
import { ClientsService } from 'src/services/ClientsService';

interface ClientForm {
  name: FormControl<string>;
}

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.scss'],
})
export class AddClientComponent {
  public clientFormGroup = new FormGroup<ClientForm>({
    name: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
  });

  constructor(
    public _activeModal: NgbActiveModal,
    private _clientsService: ClientsService
  ) {}

  public onCancelClick(): void {
    this._activeModal.close();
  }

  public onSubmit(): void {
    if (this.clientFormGroup.valid) {
      this._clientsService.addClient(<ClientInput>this.clientFormGroup.value);
      this._activeModal.close();
    }
  }
}
