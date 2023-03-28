import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Client } from 'src/models/Client';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
})
export class ClientListComponent {
  @Input() public clients: Client[] | null = [];
  constructor(private _router: Router) {}
  public onRowClick(client: Client) {
    this._router.navigate(['/client', client.id]);
  }
}
