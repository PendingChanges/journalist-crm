import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { Pitch } from 'src/models/Pitch';
import { ClientsService } from 'src/services/ClientsService';
import { PitchesService } from 'src/services/PitchesService';

@Component({
  selector: 'app-client-page',
  templateUrl: './client-page.component.html',
  styleUrls: ['./client-page.component.scss'],
})
export class ClientPageComponent implements OnInit {
  constructor(
    private _route: ActivatedRoute,
    private _clientsService: ClientsService,
    private _pitchesService: PitchesService
  ) {}

  public client?: Observable<Client>;
  public pitches$?: Observable<Pitch[]>;

  ngOnInit(): void {
    const clientId = this._route.snapshot.params['id'];
    this.client = this._clientsService.getClient(clientId);

    this.pitches$ = this._pitchesService.pitchesByClientId$(clientId);
  }
}
