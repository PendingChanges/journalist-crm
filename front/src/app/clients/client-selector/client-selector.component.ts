import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor } from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  EMPTY,
  fromEvent,
  map,
  Observable,
  startWith,
  switchMap,
  tap,
} from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';

@Component({
  selector: 'app-client-selector',
  templateUrl: './client-selector.component.html',
  styleUrls: ['./client-selector.component.scss'],
})
export class ClientSelectorComponent implements ControlValueAccessor, OnInit {
  public filteredClients$: Observable<Client[]> = EMPTY;
  @ViewChild('input', { static: true }) input!: ElementRef;

  constructor(private _clientsService: ClientsService) {}

  ngOnInit(): void {
    this.filteredClients$ = fromEvent(this.input?.nativeElement, 'keyup').pipe(
      map((e) => this.input.nativeElement.value as string),
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((val) => {
        return this._clientsService.autoComplete(val);
      })
    );
  }

  writeValue(obj: any): void {
    throw new Error('Method not implemented.');
  }
  registerOnChange(fn: any): void {
    throw new Error('Method not implemented.');
  }
  registerOnTouched(fn: any): void {
    throw new Error('Method not implemented.');
  }
  setDisabledState?(isDisabled: boolean): void {
    throw new Error('Method not implemented.');
  }
}
