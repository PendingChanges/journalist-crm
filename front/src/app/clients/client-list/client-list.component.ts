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
import { Client } from 'src/models/Client';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
})
export class ClientListComponent implements OnInit {
  initialSelection = [];
  selection: SelectionModel<Client>;

  displayedColumns: string[] = ['name', 'nbOfPitches'];
  public dataSource: MatTableDataSource<Client> | null = null;

  @Output() public clientsSelected: EventEmitter<Client[]> = new EventEmitter<
    Client[]
  >();

  @Input() public isSelectable: boolean = false;
  @Input() public set clients(value: Client[] | null) {
    this.dataSource = new MatTableDataSource<Client>(value!);
    this.dataSource.paginator = this.paginator;
  }

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  constructor(private router: Router) {
    this.selection = new SelectionModel<Client>(false, this.initialSelection);
  }
  ngOnInit(): void {
    if (this.isSelectable) {
      this.displayedColumns.push('select');
    }
  }

  public onRowClick(client: Client) {
    if (!this.isSelectable) {
      this.router.navigate(['/client', client.id]);
    } else {
      this.toggleClientSelection(client);
    }
  }

  public toggleClientSelection(client: Client) {
    this.selection.toggle(client);
    this.clientsSelected.emit(this.selection.selected);
  }
}
