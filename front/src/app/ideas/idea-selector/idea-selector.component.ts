import { Component } from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  Observable,
  OperatorFunction,
  switchMap,
} from 'rxjs';
import { Idea } from 'src/generated/graphql';
import { IdeasService } from 'src/services/IdeasService';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-idea-selector',
  templateUrl: './idea-selector.component.html',
  styleUrls: ['./idea-selector.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: IdeaSelectorComponent,
      multi: true,
    },
  ],
  standalone: true,
  imports: [ReactiveFormsModule, NgbTypeahead, FormsModule],
})
export class IdeaSelectorComponent implements ControlValueAccessor {
  constructor(private _ideasService: IdeasService) {}
  private _value: Idea | null = null;
  public set value(val: Idea | null) {
    this._value = val;
    this.onChange(val);
    this.onTouch(val);
  }
  public get value(): Idea | null {
    return this._value;
  }
  public disabled: boolean = false;
  onChange: any = () => {};
  onTouch: any = () => {};

  writeValue(val: Idea | null): void {
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

  search: OperatorFunction<string, readonly Idea[]> = (
    text$: Observable<string>
  ) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((val) => {
        return this._ideasService.autoComplete(val);
      })
    );

  formatter = (result: Idea) => result.name;
}
