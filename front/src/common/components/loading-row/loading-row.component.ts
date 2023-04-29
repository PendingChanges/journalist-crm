import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loading-row',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading-row.component.html',
  styleUrls: ['./loading-row.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoadingRowComponent {
  @Input() public loading: boolean | null = false;
}
