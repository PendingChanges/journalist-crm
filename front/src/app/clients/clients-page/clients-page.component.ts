import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';

@Component({
  selector: 'app-clients-page',
  templateUrl: './clients-page.component.html',
  styleUrls: ['./clients-page.component.scss'],
})
export class ClientsComponent {
  constructor(clientsService: ClientsService) {
    this.clients$ = clientsService.clients$;
  }

  public clients$: Observable<Client[]>;
}
