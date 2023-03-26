import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  debounceTime,
  distinctUntilChanged,
  EMPTY,
  fromEvent,
  map,
  Observable,
  OperatorFunction,
  switchMap,
} from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';

@Component({
  selector: 'app-client-selector',
  templateUrl: './client-selector.component.html',
  styleUrls: ['./client-selector.component.scss'],
})
export class ClientSelectorComponent {
  constructor(private _clientsService: ClientsService) {}

  public clientSelected: Client | null = null;

  search: OperatorFunction<string, readonly Client[]> = (
    text$: Observable<string>
  ) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((val) => {
        return this._clientsService.autoComplete(val);
      })
    );

  formatter = (result: Client) => result.name;
}
