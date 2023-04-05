import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgbActiveModal, NgbInputDatepicker } from '@ng-bootstrap/ng-bootstrap';
import { Client, Idea } from 'src/generated/graphql';
import { PitchInput } from 'src/mutations/AddPitchMutation';
import { PitchesService } from 'src/services/PitchesService';
import { EditorComponent } from '@tinymce/tinymce-angular';
import { IdeaSelectorComponent } from '../../ideas/idea-selector/idea-selector.component';
import { ClientSelectorComponent } from '../../clients/client-selector/client-selector.component';
import { TranslocoModule } from '@ngneat/transloco';

interface ClientForm {
  clientId: FormControl<string | null>;
}

interface IdeaForm {
  ideaId: FormControl<string | null>;
}

interface PitchForm {
  client: FormControl<Client | null>;
  idea: FormControl<Idea | null>;
  title: FormControl<string>;
  content: FormControl<string>;
  deadLineDate: FormControl<Date | null>;
  issueDate: FormControl<Date | null>;
}

@Component({
    selector: 'app-add-pitch',
    templateUrl: './add-pitch.component.html',
    styleUrls: ['./add-pitch.component.scss'],
    standalone: true,
    imports: [TranslocoModule, ReactiveFormsModule, ClientSelectorComponent, IdeaSelectorComponent, EditorComponent, NgbInputDatepicker]
})
export class AddPitchComponent implements OnInit {
  public data?: AddPitchDialogModel;

  public pitchFormGroup = new FormGroup<PitchForm>({
    client: new FormControl(null, {
      nonNullable: true,
      validators: Validators.required,
    }),
    idea: new FormControl(null, {
      nonNullable: true,
      validators: Validators.required,
    }),
    title: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
    content: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
    deadLineDate: new FormControl<Date | null>(null),
    issueDate: new FormControl<Date | null>(null),
  });

  constructor(
    public _activeModal: NgbActiveModal,
    private _pitchesService: PitchesService
  ) {}
  ngOnInit(): void {
    this.pitchFormGroup.patchValue({
      client: this.data?.client,
      idea: this.data?.idea,
    });

    if (this.data?.disableClient) {
      this.pitchFormGroup.controls.client.disable();
    }

    if (this.data?.disableIdea) {
      this.pitchFormGroup.controls.idea.disable();
    }
  }

  public onCancelClick(): void {
    this._activeModal.close();
  }

  public onSubmit(): void {
    if (this.pitchFormGroup.valid) {
      this._pitchesService.addPitch(<PitchInput>{
        clientId: this.pitchFormGroup.value.client?.id,
        ideaId: this.pitchFormGroup.value.idea?.id,
        content: this.pitchFormGroup.value.content,
        deadLineDate: this.pitchFormGroup.value.deadLineDate,
        issueDate: this.pitchFormGroup.value.issueDate,
        title: this.pitchFormGroup.value.title,
      });
      this._activeModal.close();
    }
  }
}

export class AddPitchDialogModel {
  constructor(
    public client: Client | null,
    public idea: Idea | null,
    public disableClient: boolean,
    public disableIdea: boolean
  ) {}
}
