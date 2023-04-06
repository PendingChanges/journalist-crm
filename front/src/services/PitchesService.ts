import { Injectable } from '@angular/core';
import { QueryRef } from 'apollo-angular';
import { map } from 'rxjs';
import {
  AllPitchesCollectionSegment,
  MutationAddPitchArgs,
  CreatePitchInput,
  QueryAllPitchesArgs,
} from 'src/generated/graphql';
import { AddPitchMutation } from 'src/mutations/AddPitchMutation';
import { AllPitchesQuery } from 'src/queries/AllPitchesQuery';
import { PitchQuery } from 'src/queries/PitchQuery';

@Injectable({
  providedIn: 'root',
})
export class PitchesService {
  constructor(
    private _allPitchesQuery: AllPitchesQuery,
    private _pitchQuery: PitchQuery,
    private _addPitchMutation: AddPitchMutation
  ) {}

  private _allPitchesQueryRef: QueryRef<
    AllPitchesCollectionSegment,
    QueryAllPitchesArgs
  > | null = null;

  private _allPitchesByClientIdQueryRef: {
    [id: string]: QueryRef<
      AllPitchesCollectionSegment,
      QueryAllPitchesArgs
    > | null;
  } = {};

  private _allPitchesByIDeaIdQueryRef: {
    [id: string]: QueryRef<
      AllPitchesCollectionSegment,
      QueryAllPitchesArgs
    > | null;
  } = {};

  public get pitches$() {
    if (!this._allPitchesQueryRef) {
      this._allPitchesQueryRef = this._allPitchesQuery.watch();
    }

    return this._allPitchesQueryRef.valueChanges.pipe(
      map((result: any) => result.data.allPitches.items)
    );
  }

  public pitchesByClientId$(clientId: string) {
    if (!this._allPitchesByClientIdQueryRef[clientId]) {
      this._allPitchesByClientIdQueryRef[clientId] =
        this._allPitchesQuery.watch({
          clientId: clientId,
          ideaId: null,
        });
    }
    return this._allPitchesByClientIdQueryRef[clientId]?.valueChanges.pipe(
      map((result: any) => result.data.allPitches.items)
    );
  }

  public pitchesByIdeaId$(ideaId: string) {
    if (!this._allPitchesByIDeaIdQueryRef[ideaId]) {
      this._allPitchesByIDeaIdQueryRef[ideaId] = this._allPitchesQuery.watch({
        ideaId: ideaId,
        clientId: null,
      });
    }
    return this._allPitchesByIDeaIdQueryRef[ideaId]?.valueChanges.pipe(
      map((result: any) => result.data.allPitches.items)
    );
  }

  public refreshPitches(): void {
    this._allPitchesQueryRef?.refetch();
  }

  public getPitch(id: string) {
    return this._pitchQuery
      .watch({
        id: id,
      })
      .valueChanges.pipe(map((result: any) => result.data.pitch));
  }

  public addPitch(value: CreatePitchInput) {
    this._addPitchMutation.mutate(value).subscribe(() => {
      this.refreshPitches();
    });
  }
}
