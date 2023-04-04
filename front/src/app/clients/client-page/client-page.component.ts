import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { Pitch } from 'src/models/Pitch';
import { ClientsService } from 'src/services/ClientsService';
import { PitchesService } from 'src/services/PitchesService';
import { PitchListComponent } from '../../pitches/pitch-list/pitch-list.component';
import { ClientActionMenuComponent } from '../client-action-menu/client-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';
import { NgIf, AsyncPipe } from '@angular/common';

@Component({
    selector: 'app-client-page',
    templateUrl: './client-page.component.html',
    styleUrls: ['./client-page.component.scss'],
    standalone: true,
    imports: [NgIf, TranslocoModule, ClientActionMenuComponent, PitchListComponent, AsyncPipe]
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
