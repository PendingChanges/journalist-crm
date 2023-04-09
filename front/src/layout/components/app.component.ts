import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';
import { NavbarComponent } from './navbar/navbar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [
    TranslocoModule,
    NavbarComponent,
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
  ],
})
export class AppComponent {
  title = 'journalist-crm';
}
