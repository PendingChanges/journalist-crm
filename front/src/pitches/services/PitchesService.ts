import { Injectable } from '@angular/core';
import { MutationResult, QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import {
  AllPitchesCollectionSegment,
  Pitch,
  PitchAddedPayload,
  DeletePitchInput,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';
import { AddPitchMutation } from 'src/pitches/mutations/AddPitchMutation';
import { CreatePitchInput } from 'src/models/generated/graphql';
import { AllPitchesQuery } from 'src/pitches/queries/AllPitchesQuery';
import { PitchQuery } from 'src/pitches/queries/PitchQuery';
import { ApolloQueryResult } from '@apollo/client/core';

@Injectable({
  providedIn: 'root',
})
export class PitchesService {
  private _allPitchesQueryRef: QueryRef<
    { allPitches: AllPitchesCollectionSegment },
    QueryAllPitchesArgs
  > | null = null;

  private _allPitchesByClientIdQueryRef: {
    [id: string]: QueryRef<
      { allPitches: AllPitchesCollectionSegment },
      QueryAllPitchesArgs
    > | null;
  } = {};

  private _allPitchesByIDeaIdQueryRef: {
    [id: string]: QueryRef<
      { allPitches: AllPitchesCollectionSegment },
      QueryAllPitchesArgs
    > | null;
  } = {};

  constructor(
    private _allPitchesQuery: AllPitchesQuery,
    private _pitchQuery: PitchQuery,
    private _addPitchMutation: AddPitchMutation
  ) {
    this._allPitchesQueryRef = this._allPitchesQuery.watch();
    this.pitchListResult$ = this._allPitchesQueryRef.valueChanges;
  }

  public pitchListResult$: Observable<
    ApolloQueryResult<{ allPitches: AllPitchesCollectionSegment }>
  >;

  public refreshPitches(args: QueryAllPitchesArgs): void {
    this._allPitchesQueryRef?.refetch(args);
  }

  public getPitch(id: string) {
    return this._pitchQuery.watch({
      id: id,
    }).valueChanges;
  }

  public addPitch(
    value: CreatePitchInput
  ): Observable<MutationResult<{ addPitch: PitchAddedPayload }>> {
    return this._addPitchMutation.mutate(value);
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
}
