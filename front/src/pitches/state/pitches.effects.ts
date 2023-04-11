import { inject } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { MutationResult } from 'apollo-angular';
import { catchError, switchMap, map, of, tap } from 'rxjs';
import {
  AllPitchesCollectionSegment,
  Pitch,
  PitchAddedPayload,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';
import { PitchesService } from 'src/pitches/services/PitchesService';
import { PitchesActions } from './pitches.actions';
import { Router } from '@angular/router';

export const loadPitches = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(PitchesActions.loadPitchList),
      switchMap((a: { args: QueryAllPitchesArgs; date?: Date }) => {
        pitchesService.refreshPitches(a.args);
        return pitchesService.pitchListResult$.pipe(
          map((pitchListResult) =>
            PitchesActions.pitchListLoadedSuccess({
              pitches: pitchListResult.data.allPitches.items || [],
            })
          ),
          catchError((result: ApolloQueryResult<AllPitchesCollectionSegment>) =>
            of(
              PitchesActions.pitchListLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const loadPitch = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(PitchesActions.loadPitch),
      switchMap(({ pitchId }) => {
        return pitchesService.getPitch(pitchId).pipe(
          map((pitchResult) =>
            PitchesActions.pitchLoadedSuccess({
              pitch: pitchResult.data.pitch,
            })
          ),
          catchError((result: ApolloQueryResult<{ pitch: Pitch }>) =>
            of(
              PitchesActions.pitchLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const addPitch = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(PitchesActions.addPitch),
      switchMap((addPitch) => {
        return pitchesService.addPitch(addPitch).pipe(
          map((addPitchResult) =>
            PitchesActions.pitchAddedSuccess({
              payload: <PitchAddedPayload>addPitchResult.data?.addPitch,
              args: <QueryAllPitchesArgs>{
                skip: 0,
                sortBy: 'name',
                take: 10,
              },
              date: new Date(),
            })
          ),
          catchError(
            (result: MutationResult<{ addPitch: PitchAddedPayload }>) =>
              of(
                PitchesActions.pitchAddedFailure({
                  errors: result.errors?.map((e) => e.message) || [
                    'Unknown error',
                  ],
                })
              )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const removePitch = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(PitchesActions.removePitch),
      switchMap((removePitch) => {
        return pitchesService.removePitch(removePitch).pipe(
          map((removePitchResult) =>
            PitchesActions.pitchRemovedSuccess({
              payload: <string>removePitchResult.data?.removePitch,
            })
          ),
          catchError((result: MutationResult<{ removePitch: string }>) =>
            of(
              PitchesActions.pitchRemovedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const redirectToPitch = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(PitchesActions.pitchAddedSuccess),
      tap((result) => {
        router.navigate(['/pitches', result.payload.pitchId]);
      })
    );
  },
  { functional: true, dispatch: false }
);

export const redirectToPitches = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(PitchesActions.pitchRemovedSuccess),
      tap(() => {
        router.navigate(['/pitches']);
      })
    );
  },
  { functional: true, dispatch: false }
);
