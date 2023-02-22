import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { TranslocoService } from '@ngneat/transloco';
import { Subject } from 'rxjs';

@Injectable()
export class MyCustomPaginatorIntl implements MatPaginatorIntl {
  changes = new Subject<void>();

  constructor(private _translocoService: TranslocoService) {}

  firstPageLabel = this._translocoService.translate('common.first_page');
  itemsPerPageLabel = this._translocoService.translate('common.items_per_page');
  lastPageLabel = this._translocoService.translate('common.last_page');
  nextPageLabel = this._translocoService.translate('common.next_page');
  previousPageLabel = this._translocoService.translate('common.previous_page');

  getRangeLabel(page: number, pageSize: number, length: number): string {
    if (length === 0) {
      return this._translocoService.translate('common.range_page', {
        first: '1',
        last: '1',
      });
    }
    const amountPages = Math.ceil(length / pageSize);
    return this._translocoService.translate('common.range_page', {
      first: page + 1,
      last: amountPages,
    });
  }
}
