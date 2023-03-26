import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { Idea } from 'src/models/Idea';
import { PitchInput } from 'src/mutations/AddPitchMutation';
import { ClientsService } from 'src/services/ClientsService';
import { IdeasService } from 'src/services/IdeasService';
import { PitchesService } from 'src/services/PitchesService';

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
    private _clientsService: ClientsService,
    private _ideasService: IdeasService,
    private _pitchesService: PitchesService
  ) {}
  ngOnInit(): void {
    this.pitchFormGroup.patchValue({
      client: this.data?.client,
      idea: this.data?.idea,
    });
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
  constructor(public client: Client | null, public idea: Idea | null) {}
}
