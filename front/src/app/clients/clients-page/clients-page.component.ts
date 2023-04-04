import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';
import { AsyncPipe } from '@angular/common';
import { ClientListComponent } from '../client-list/client-list.component';
import { ClientsActionMenuComponent } from '../clients-action-menu/clients-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-clients-page',
    templateUrl: './clients-page.component.html',
    styleUrls: ['./clients-page.component.scss'],
    standalone: true,
    imports: [TranslocoModule, ClientsActionMenuComponent, ClientListComponent, AsyncPipe]
})
export class ClientsComponent {
  constructor(clientsService: ClientsService) {
    this.clients$ = clientsService.clients$;
  }

  public clients$: Observable<Client[]>;
}
