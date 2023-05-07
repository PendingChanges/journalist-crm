import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Idea } from 'src/models/generated/graphql';
import { DeleteIdeaButtonComponent } from '../delete-idea-button/delete-idea-button.component';
import { AddPitchButtonComponent } from 'src/pitches/components/add-pitch-button/add-pitch-button.component';
import { ModifyIdeaButtonComponent } from '../modify-idea-button/modify-idea-button.component';

@Component({
  selector: 'app-idea-action-menu',
  templateUrl: './idea-action-menu.component.html',
  styleUrls: ['./idea-action-menu.component.scss'],
  standalone: true,
  imports: [
    AddPitchButtonComponent,
    DeleteIdeaButtonComponent,
    ModifyIdeaButtonComponent,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class IdeaActionMenuComponent {
  @Input({ required: true }) public idea: Idea | null = null;
  @Input() public disableDeleteButton: boolean = true;
}
