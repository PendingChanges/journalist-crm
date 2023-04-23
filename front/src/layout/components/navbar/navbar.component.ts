import { Component } from '@angular/core';
import { LanguagePickerComponent } from '../language-picker/language-picker.component';
import { NgbNavbar } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from '../profile/profile.component';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    standalone: true,
    imports: [NgbNavbar, LanguagePickerComponent, ProfileComponent]
})
export class NavbarComponent {}
