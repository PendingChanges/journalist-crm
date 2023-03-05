import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
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
    public _dialogRef: MatDialogRef<AddIdeaComponent>,
    private _ideasService: IdeasService
  ) {}

  public onCancelClick(): void {
    this._dialogRef.close();
  }

  public onSubmit(): void {
    if (this.ideaFormGroup.valid) {
      this._ideasService.addIdea(<IdeaInput>this.ideaFormGroup.value);
      this._dialogRef.close();
    }
  }
}
