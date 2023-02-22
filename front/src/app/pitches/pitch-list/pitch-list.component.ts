import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Pitch } from 'src/models/Pitch';

@Component({
  selector: 'app-pitch-list',
  templateUrl: './pitch-list.component.html',
  styleUrls: ['./pitch-list.component.scss'],
})
export class PitchListComponent implements OnInit {
  public displayedColumns: string[] = [];
  public dataSource?: MatTableDataSource<Pitch>;

  @Input() public showClient: boolean = false;
  @Input() public showIdea: boolean = false;

  @Input() public set pitches(value: Pitch[] | null) {
    this.dataSource = new MatTableDataSource<Pitch>(value!);
    this.dataSource.paginator = this.paginator!;
  }

  @ViewChild(MatPaginator) paginator?: MatPaginator;
  constructor(private router: Router) {}
  ngOnInit(): void {
    this.displayedColumns = ['title', 'content'];
    if (this.showIdea) {
      this.displayedColumns.push('idea');
    }
    if (this.showClient) {
      this.displayedColumns.push('client');
    }
  }

  public navigate(pitch: Pitch) {
    this.router.navigate(['/pitch', pitch.id]);
  }
}
