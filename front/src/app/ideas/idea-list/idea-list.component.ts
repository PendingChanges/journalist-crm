import {
  Component,
  Input,
} from '@angular/core';
import { Router } from '@angular/router';
import { Idea } from '../../../models/Idea';
import { NgFor, DecimalPipe } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-idea-list',
    templateUrl: './idea-list.component.html',
    styleUrls: ['./idea-list.component.scss'],
    standalone: true,
    imports: [TranslocoModule, NgFor, DecimalPipe]
})
export class IdeaListComponent {
  @Input() public ideas: Idea[] | null = [];

  constructor(private router: Router) {}

  public onRowClick(idea: Idea) {
    this.router.navigate(['/ideas', idea.id]);
  }
}
