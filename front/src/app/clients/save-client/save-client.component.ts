import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslocoService } from '@ngneat/transloco';
import { Client } from 'src/models/Client';
import { ClientInput } from 'src/mutations/AddClientMutation';
import { RenameClientInput } from 'src/mutations/RenameClientMutation';
import { ClientsService } from 'src/services/ClientsService';

interface ClientForm {
  name: FormControl<string>;
}

@Component({
  selector: 'app-save-client',
  templateUrl: './save-client.component.html',
  styleUrls: ['./save-client.component.scss'],
})
export class SaveClientComponent implements OnInit {
  public data?: SaveClientModel;
  public clientFormGroup = new FormGroup<ClientForm>({
    name: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
  });

  constructor(
    public _activeModal: NgbActiveModal,
    private _clientsService: ClientsService,
    private _translocoService: TranslocoService
  ) {}
  ngOnInit(): void {
    if (this.data?.type === 'modify' && this.data?.client != null) {
      this.clientFormGroup.patchValue({
        name: this.data?.client.name,
      });
    }
  }

  public onCancelClick(): void {
    this._activeModal.close();
  }

  public onSubmit(): void {
    if (this.clientFormGroup.valid) {
      if (this.data?.type === 'add') {
        this._clientsService.addClient(<ClientInput>this.clientFormGroup.value);
      }

      if (this.data?.type === 'modify') {
        this._clientsService.modifyClient(<RenameClientInput>{
          id: this.data?.client?.id,
          ...this.clientFormGroup.value,
        });
      }
      this._activeModal.close();
    }
  }

  public getTitle(): string {
    return this.data?.type === 'add'
      ? this._translocoService.translate('clients.add_client')
      : this._translocoService.translate('clients.modify_client', {
          clientName: this.data?.client?.name,
        });
  }
}
export type SaveType = 'add' | 'modify';

export class SaveClientModel {
  constructor(public type: SaveType, public client: Client | null) {}
}
