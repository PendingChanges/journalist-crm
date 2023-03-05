import { SelectionModel } from '@angular/cdk/collections';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Idea } from '../../../models/Idea';

@Component({
  selector: 'app-idea-list',
  templateUrl: './idea-list.component.html',
  styleUrls: ['./idea-list.component.scss'],
})
export class IdeaListComponent implements OnInit {
  initialSelection = [];
  selection: SelectionModel<Idea>;
  public dataSource?: MatTableDataSource<Idea>;
  public displayedColumns: string[] = ['name', 'description', 'nbOfPitches'];

  @Input() public isSelectable: boolean = false;
  @Input() public set ideas(value: Idea[] | null) {
    this.dataSource = new MatTableDataSource<Idea>(value!);
    this.dataSource.paginator = this.paginator!;
  }

  @Output() public ideasSelected: EventEmitter<Idea[]> = new EventEmitter<
    Idea[]
  >();

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  constructor(private router: Router) {
    this.selection = new SelectionModel<Idea>(false, this.initialSelection);
  }
  ngOnInit(): void {
    if (this.isSelectable) {
      this.displayedColumns.push('select');
    }
  }

  public onRowClick(idea: Idea) {
    if (!this.isSelectable) {
      this.router.navigate(['/idea', idea.id]);
    } else {
      this.toggleIdeaSelection(idea);
    }
  }

  public toggleIdeaSelection(idea: Idea) {
    this.selection.toggle(idea);
    this.ideasSelected.emit(this.selection.selected);
  }
}
