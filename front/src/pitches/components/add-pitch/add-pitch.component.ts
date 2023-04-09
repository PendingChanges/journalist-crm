import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { NgbActiveModal, NgbInputDatepicker } from '@ng-bootstrap/ng-bootstrap';
import { Client, Idea, CreatePitchInput } from 'src/models/generated/graphql';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { EditorComponent } from '@tinymce/tinymce-angular';
import { ClientSelectorComponent } from '../../../clients/components/client-selector/client-selector.component';
import { TranslocoModule } from '@ngneat/transloco';
import { IdeaSelectorComponent } from 'src/ideas/components/idea-selector/idea-selector.component';
import { Store } from '@ngrx/store';
import { PitchesActions } from 'src/pitches/state/pitches.actions';

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
  imports: [
    TranslocoModule,
    ReactiveFormsModule,
    ClientSelectorComponent,
    IdeaSelectorComponent,
    EditorComponent,
    NgbInputDatepicker,
  ],
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

  constructor(public _activeModal: NgbActiveModal, private _store: Store) {}
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
      this._store.dispatch(
        PitchesActions.addPitch(<CreatePitchInput>{
          clientId: this.pitchFormGroup.value.client?.id,
          ideaId: this.pitchFormGroup.value.idea?.id,
          content: this.pitchFormGroup.value.content,
          deadLineDate: this.pitchFormGroup.value.deadLineDate,
          issueDate: this.pitchFormGroup.value.issueDate,
          title: this.pitchFormGroup.value.title,
        })
      );
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
