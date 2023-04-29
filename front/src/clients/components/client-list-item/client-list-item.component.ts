import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Client } from 'src/models/generated/graphql';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-client-list-item',
  standalone: true,
  imports: [CommonModule, TranslocoModule],
  templateUrl: './client-list-item.component.html',
  styleUrls: ['./client-list-item.component.scss'],
})
export class ClientListItemComponent {
  constructor(private _router: Router) {}

  @Input() public client: Client | null = null;

  public onRowClick(client: Client | null) {
    if (!client) return;

    this._router.navigate(['/clients', client.id]);
  }
}
