import { Component, Input } from '@angular/core';
import { Client } from 'src/models/Client';
import { ClientDeleteButtonComponent } from '../client-delete-button/client-delete-button.component';
import { AddPitchButtonComponent } from '../../pitches/add-pitch/add-pitch-button/add-pitch-button.component';
import { ClientModifyButtonComponent } from '../client-modify-button/client-modify-button.component';

@Component({
    selector: 'app-client-action-menu',
    templateUrl: './client-action-menu.component.html',
    styleUrls: ['./client-action-menu.component.scss'],
    standalone: true,
    imports: [ClientModifyButtonComponent, AddPitchButtonComponent, ClientDeleteButtonComponent]
})
export class ClientActionMenuComponent {
  @Input() public client: Client | null = null;
  @Input() public disableDeleteButton: boolean = true;
}
