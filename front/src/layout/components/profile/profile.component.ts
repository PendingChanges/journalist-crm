import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  NgbDropdown,
  NgbDropdownItem,
  NgbDropdownMenu,
  NgbDropdownToggle,
} from '@ng-bootstrap/ng-bootstrap';
import { TranslocoModule } from '@ngneat/transloco';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakProfile } from 'keycloak-js';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    NgbDropdown,
    NgbDropdownMenu,
    NgbDropdownToggle,
    NgbDropdownItem,
    TranslocoModule,
  ],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  public isLoggedIn: boolean = false;
  public profile: KeycloakProfile | null = null;

  constructor(private keycloak: KeycloakService) {}
  async ngOnInit() {
    this.isLoggedIn = await this.keycloak.isLoggedIn();
    this.profile = await this.keycloak.loadUserProfile();
  }

  public async onDisconnectClick() {
    await this.keycloak.logout();
  }

  public get getDisplayName(): string {
    return `${this.profile?.firstName} ${this.profile?.lastName}`;
  }
}
