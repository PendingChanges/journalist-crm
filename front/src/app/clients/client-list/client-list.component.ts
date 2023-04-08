import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Client } from 'src/generated/graphql';
import { NgFor, DecimalPipe } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
  standalone: true,
  imports: [TranslocoModule, NgFor, DecimalPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientListComponent {
  @Input() public clients: readonly Client[] | null = [];
  constructor(private _router: Router) {}
  public onRowClick(client: Client) {
    this._router.navigate(['/clients', client.id]);
  }
}
