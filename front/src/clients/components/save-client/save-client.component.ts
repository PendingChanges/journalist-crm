import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslocoService, TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { Client, RenameClientInput } from 'src/models/generated/graphql';
import { CreateClientInput } from 'src/models/generated/graphql';
import { ClientsActions } from 'src/clients/state/clients.actions';

interface ClientForm {
  name: FormControl<string>;
}

@Component({
  selector: 'app-save-client',
  templateUrl: './save-client.component.html',
  styleUrls: ['./save-client.component.scss'],
  standalone: true,
  imports: [TranslocoModule, ReactiveFormsModule],
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
    private _translocoService: TranslocoService,
    private _store: Store
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
        this._store.dispatch(
          ClientsActions.addClient(
            <CreateClientInput>this.clientFormGroup.value
          )
        );
      }

      if (this.data?.type === 'modify') {
        this._store.dispatch(
          ClientsActions.renameClient(<RenameClientInput>{
            id: this.data?.client?.id,
            newName: this.clientFormGroup.value.name,
          })
        );
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
