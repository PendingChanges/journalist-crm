import { Component, Input } from '@angular/core';
import { Client } from 'src/models/Client';

@Component({
  selector: 'app-client-action-menu',
  templateUrl: './client-action-menu.component.html',
  styleUrls: ['./client-action-menu.component.scss'],
})
export class ClientActionMenuComponent {
  @Input() public client: Client | null = null;
}
