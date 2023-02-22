import { Component, OnInit } from '@angular/core';
import { AvailableLangs, TranslocoService } from '@ngneat/transloco';

@Component({
  selector: 'app-language-picker',
  templateUrl: './language-picker.component.html',
  styleUrls: ['./language-picker.component.scss'],
})
export class LanguagePickerComponent implements OnInit {
  public languages?: string[];
  public get currentLanguage(): string {
    return this._transloco.getActiveLang();
  }
  constructor(private _transloco: TranslocoService) {}
  ngOnInit(): void {
    this.languages = this._transloco.getAvailableLangs() as string[];
  }

  public onLanguageClick(language: string) {
    this._transloco.setActiveLang(language);
  }
}
