import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  EMPTY,
  map,
  Observable,
  OperatorFunction,
  switchMap,
} from 'rxjs';
import { Client } from 'src/models/Client';
import { ClientsService } from 'src/services/ClientsService';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-client-selector',
    templateUrl: './client-selector.component.html',
    styleUrls: ['./client-selector.component.scss'],
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: ClientSelectorComponent, multi: true },
    ],
    standalone: true,
    imports: [ReactiveFormsModule, NgbTypeahead, FormsModule]
})
export class ClientSelectorComponent implements ControlValueAccessor {
  constructor(private _clientsService: ClientsService) {}
  private _value: Client | null = null;
  public set value(val: Client | null) {
    this._value = val;
    this.onChange(val);
    this.onTouch(val);
  }
  public get value(): Client | null {
    return this._value;
  }
  public disabled: boolean = false;
  onChange: any = () => {};
  onTouch: any = () => {};

  writeValue(val: Client | null): void {
    this.value = val;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

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
