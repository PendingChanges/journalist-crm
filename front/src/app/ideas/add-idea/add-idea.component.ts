import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IdeaInput } from 'src/mutations/AddIdeaMutation';
import { IdeasService } from 'src/services/IdeasService';

interface IdeaForm {
  name: FormControl<string>;
  description?: FormControl<string>;
}

@Component({
    selector: 'app-add-idea',
    templateUrl: './add-idea.component.html',
    styleUrls: ['./add-idea.component.scss'],
    standalone: true,
    imports: [ReactiveFormsModule]
})
export class AddIdeaComponent {
  public ideaFormGroup = new FormGroup<IdeaForm>({
    name: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
    description: new FormControl('', { nonNullable: true }),
  });

  constructor(
    public _activeModal: NgbActiveModal,
    private _ideasService: IdeasService
  ) {}

  public onCancelClick(): void {
    this._activeModal.close();
  }

  public onSubmit(): void {
    if (this.ideaFormGroup.valid) {
      this._ideasService.addIdea(<IdeaInput>this.ideaFormGroup.value);
      this._activeModal.close();
    }
  }
}
