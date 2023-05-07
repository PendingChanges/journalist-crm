import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Client } from 'src/models/generated/graphql';

import {
  SaveClientComponent,
  SaveClientModel,
} from '../save-client/save-client.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-client-modify-button',
  templateUrl: './client-modify-button.component.html',
  styleUrls: ['./client-modify-button.component.scss'],
  standalone: true,
  imports: [TranslocoModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientModifyButtonComponent {
  @Input({ required: true }) public client: Client | null = null;
  constructor(private _modalService: NgbModal) {}
  openDialog(): void {
    const dialogRef = this._modalService.open(SaveClientComponent);
    dialogRef.componentInstance.data = new SaveClientModel(
      'modify',
      this.client
    );
  }
}
