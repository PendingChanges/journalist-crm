import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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
  public clients$?: Observable<any>;
  public ideas$?: Observable<any>;

  public clientFormGroup = new FormGroup<ClientForm>({
    clientId: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
  });

  public ideaFormGroup = new FormGroup<IdeaForm>({
    ideaId: new FormControl('', {
      nonNullable: true,
      validators: Validators.required,
    }),
  });

  public pitchFormGroup = new FormGroup<PitchForm>({
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
    private _dialogRef: MatDialogRef<AddPitchComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddPitchDialogModel,
    private _clientsService: ClientsService,
    private _ideasService: IdeasService,
    private _pitchesService: PitchesService
  ) {
    this.clientFormGroup.patchValue({ clientId: data.client?.id });
    this.ideaFormGroup.patchValue({ ideaId: data.idea?.id });
  }
  ngOnInit(): void {
    this.clients$ = this._clientsService.clients$;
    this.ideas$ = this._ideasService.ideas$;
  }

  public onClientsSelected(clients: Client[]): void {
    if (clients.length == 0) {
      this.clientFormGroup.patchValue({ clientId: null });
      return;
    }

    this.clientFormGroup.patchValue({ clientId: clients[0].id });
  }

  public onIdeasSelected(ideas: Idea[]) {
    if (ideas.length == 0) {
      this.ideaFormGroup.patchValue({ ideaId: null });
      return;
    }

    this.ideaFormGroup.patchValue({ ideaId: ideas[0].id });
  }

  public onSaveClick() {
    if (
      this.ideaFormGroup.valid &&
      this.clientFormGroup.valid &&
      this.pitchFormGroup.valid
    ) {
      this._pitchesService.addPitch(<PitchInput>{
        clientId: this.clientFormGroup.value.clientId,
        ideaId: this.ideaFormGroup.value.ideaId,
        content: this.pitchFormGroup.value.content,
        deadLineDate: this.pitchFormGroup.value.deadLineDate,
        issueDate: this.pitchFormGroup.value.issueDate,
        title: this.pitchFormGroup.value.title,
      });
      this._dialogRef.close();
    }
  }
}

export class AddPitchDialogModel {
  constructor(public client: Client | null, public idea: Idea | null) {}
}
