import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Idea } from 'src/models/generated/graphql';
import { Router } from '@angular/router';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
  selector: 'app-idea-list-item',
  standalone: true,
  imports: [CommonModule, TranslocoModule],
  templateUrl: './idea-list-item.component.html',
  styleUrls: ['./idea-list-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class IdeaListItemComponent {
  @Input() public idea: Idea | null = null;
  constructor(private _router: Router) {}

  public onRowClick(idea: Idea | null) {
    if (!idea) return;

    this._router.navigate(['/ideas', idea.id]);
  }
}
