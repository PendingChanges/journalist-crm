import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AddClientButtonComponent } from '../add-client-button/add-client-button.component';

@Component({
  selector: 'app-clients-action-menu',
  templateUrl: './clients-action-menu.component.html',
  styleUrls: ['./clients-action-menu.component.scss'],
  standalone: true,
  imports: [AddClientButtonComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientsActionMenuComponent {}
