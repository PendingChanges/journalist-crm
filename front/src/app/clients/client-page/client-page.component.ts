import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';

@Component({
  selector: 'app-client-page',
  templateUrl: './client-page.component.html',
  styleUrls: ['./client-page.component.scss'],
})
export class ClientPageComponent implements OnInit {
  constructor(
    private _route: ActivatedRoute,
    private _clientsService: ClientsService
  ) {}

  public client?: Observable<Client>;

  ngOnInit(): void {
    this.client = this._clientsService.getClient(
      this._route.snapshot.params['id']
    );
  }
}
