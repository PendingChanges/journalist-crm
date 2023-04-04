import { Component, Input } from '@angular/core';
import { Idea } from 'src/models/Idea';
import { DeleteIdeaButtonComponent } from '../delete-idea-button/delete-idea-button.component';
import { AddPitchButtonComponent } from '../../pitches/add-pitch/add-pitch-button/add-pitch-button.component';

@Component({
    selector: 'app-idea-action-menu',
    templateUrl: './idea-action-menu.component.html',
    styleUrls: ['./idea-action-menu.component.scss'],
    standalone: true,
    imports: [AddPitchButtonComponent, DeleteIdeaButtonComponent]
})
export class IdeaActionMenuComponent {
  @Input() public idea: Idea | null = null;
  @Input() public disableDeleteButton: boolean = true;
}
