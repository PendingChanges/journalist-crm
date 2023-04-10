import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import {
  CreateIdeaInput,
  Idea,
  ModifyIdeaInput,
} from 'src/models/generated/graphql';
import { IdeasActions } from 'src/ideas/state/ideas.actions';
import { SaveType } from 'src/models/SaveType';
import { TranslocoService } from '@ngneat/transloco';

interface IdeaForm {
  name: FormControl<string>;
  description: FormControl<string | null>;
}

@Component({
  selector: 'app-save-idea',
  templateUrl: './save-idea.component.html',
  styleUrls: ['./save-idea.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class SaveIdeaComponent implements OnInit {
  public data?: SaveIdeaModel;
  public ideaFormGroup = new FormGroup<IdeaForm>({
    name: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
    description: new FormControl('', { nonNullable: true }),
  });

  constructor(
    public _activeModal: NgbActiveModal,
    private _translocoService: TranslocoService,
    private _store: Store
  ) {}

  ngOnInit(): void {
    if (this.data?.type === 'modify' && this.data?.idea != null) {
      this.ideaFormGroup.patchValue({
        name: this.data?.idea.name,
        description: this.data?.idea.description,
      });
    }
  }

  public onCancelClick(): void {
    this._activeModal.close();
  }

  public onSubmit(): void {
    if (this.ideaFormGroup.valid) {
      if (this.data?.type === 'add') {
        this._store.dispatch(
          IdeasActions.addIdea(<CreateIdeaInput>this.ideaFormGroup.value)
        );
      }

      if (this.data?.type === 'modify') {
        this._store.dispatch(
          IdeasActions.modifyIdea(<ModifyIdeaInput>{
            id: this.data?.idea?.id,
            newName: this.ideaFormGroup.value.name,
            newDescription: this.ideaFormGroup.value.description,
          })
        );

        this._activeModal.close();
      }
    }
  }

  public getTitle(): string {
    return this.data?.type === 'add'
      ? this._translocoService.translate('clients.add_client')
      : this._translocoService.translate('clients.modify_client', {
          clientName: this.data?.idea?.name,
        });
  }
}

export class SaveIdeaModel {
  constructor(public type: SaveType, public idea: Idea | null) {}
}
