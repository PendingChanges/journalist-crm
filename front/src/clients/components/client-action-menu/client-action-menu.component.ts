import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Client } from 'src/models/generated/graphql';
import { ClientDeleteButtonComponent } from '../client-delete-button/client-delete-button.component';
import { ClientModifyButtonComponent } from '../client-modify-button/client-modify-button.component';
import { AddPitchButtonComponent } from 'src/pitches/components/add-pitch-button/add-pitch-button.component';

@Component({
  selector: 'app-client-action-menu',
  templateUrl: './client-action-menu.component.html',
  styleUrls: ['./client-action-menu.component.scss'],
  standalone: true,
  imports: [
    ClientModifyButtonComponent,
    AddPitchButtonComponent,
    ClientDeleteButtonComponent,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientActionMenuComponent {
  @Input() public client: Client | null = null;
  @Input() public disableDeleteButton: boolean = true;
}
