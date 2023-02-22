import { Injectable } from '@angular/core';
import { QueryRef } from 'apollo-angular';
import { map } from 'rxjs';
import { AllPitchesQuery } from 'src/queries/AllPitchesQuery';

@Injectable({
  providedIn: 'root',
})
export class PitchesService {
  constructor(private _allPitchesQuery: AllPitchesQuery) {}

  private _allPitchesQueryRef: QueryRef<any> | null = null;

  public get pitches$() {
    if (!this._allPitchesQueryRef) {
      this._allPitchesQueryRef = this._allPitchesQuery.watch();
    }

    return this._allPitchesQueryRef.valueChanges.pipe(
      map((result: any) => result.data.allPitches.items)
    );
  }

  public refreshPitches(): void {
    this._allPitchesQueryRef?.refetch();
  }
}
