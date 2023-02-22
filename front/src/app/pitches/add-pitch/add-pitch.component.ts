import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { Idea } from 'src/models/Idea';
import { ClientsService } from 'src/services/ClientsService';
import { IdeasService } from 'src/services/IdeasService';

@Component({
  selector: 'app-add-pitch',
  templateUrl: './add-pitch.component.html',
  styleUrls: ['./add-pitch.component.scss'],
})
export class AddPitchComponent implements OnInit {
  public clients$?: Observable<any>;
  public ideas$?: Observable<any>;

  @Input() public client?: Client;
  @Input() public idea?: Idea;

  constructor(
    private _dialogRef: MatDialogRef<AddPitchComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddPitchDialogModel,
    private _clientsService: ClientsService,
    private _ideasService: IdeasService
  ) {
    this.client = data.client;
    this.idea = data.idea;
  }
  ngOnInit(): void {
    this.clients$ = this._clientsService.clients$;
    this.ideas$ = this._ideasService.ideas$;
  }
}

export class AddPitchDialogModel {
  constructor(public client?: Client, public idea?: Idea) {}
}
