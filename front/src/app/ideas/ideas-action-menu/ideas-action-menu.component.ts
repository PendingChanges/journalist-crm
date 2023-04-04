import { Component } from '@angular/core';
import { AddIdeaButtonComponent } from '../add-idea-button/add-idea-button.component';

@Component({
    selector: 'app-ideas-action-menu',
    templateUrl: './ideas-action-menu.component.html',
    styleUrls: ['./ideas-action-menu.component.scss'],
    standalone: true,
    imports: [AddIdeaButtonComponent]
})
export class IdeasActionMenuComponent {

}
