import { Component, Input } from '@angular/core';
import { Idea } from 'src/models/Idea';

@Component({
  selector: 'app-idea-action-menu',
  templateUrl: './idea-action-menu.component.html',
  styleUrls: ['./idea-action-menu.component.scss'],
})
export class IdeaActionMenuComponent {
  @Input() public idea: Idea | null = null;
}
