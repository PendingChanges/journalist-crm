import {
  Component,
  Input,
} from '@angular/core';
import { Router } from '@angular/router';
import { Idea } from '../../../models/Idea';

@Component({
  selector: 'app-idea-list',
  templateUrl: './idea-list.component.html',
  styleUrls: ['./idea-list.component.scss'],
})
export class IdeaListComponent {
  @Input() public ideas: Idea[] | null = [];

  constructor(private router: Router) {}

  public onRowClick(idea: Idea) {
    this.router.navigate(['/ideas', idea.id]);
  }
}
