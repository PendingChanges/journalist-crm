import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  AddClientMutation,
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
    private _dialogRef: MatDialogRef<AddClientComponent>,
    private _clientsService: ClientsService
  ) {}

  public onCancelClick(): void {
    this._dialogRef.close();
  }

  public onSubmit(): void {
    if (this.clientFormGroup.valid) {
      this._clientsService.addClient(<ClientInput>this.clientFormGroup.value);
      this._dialogRef.close();
    }
  }
}
