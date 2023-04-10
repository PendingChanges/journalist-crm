import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { MutationResult } from 'apollo-angular';
import { catchError, switchMap, map, of, tap } from 'rxjs';
import {
  AllIdeasCollectionSegment,
  AllPitchesCollectionSegment,
  Idea,
  IdeaAddedPayload,
  QueryAllIdeasArgs,
} from 'src/models/generated/graphql';
import { IdeasService } from 'src/ideas/services/IdeasService';
import { IdeasActions } from './ideas.actions';
import { Router } from '@angular/router';
import { ApolloQueryResult } from '@apollo/client/core';
import { PitchesService } from 'src/pitches/services/PitchesService';

export const loadIdeas = createEffect(
  (actions$ = inject(Actions), ideasService = inject(IdeasService)) => {
    return actions$.pipe(
      ofType(IdeasActions.loadIdeaList),
      switchMap((a: { args: QueryAllIdeasArgs; date?: Date }) => {
        ideasService.refreshIdeas(a.args);
        return ideasService.ideaListResult$.pipe(
          map((ideaListResult) =>
            IdeasActions.ideaListLoadedSuccess({
              ideas: ideaListResult.data.allIdeas.items || [],
            })
          ),
          catchError((result: ApolloQueryResult<AllIdeasCollectionSegment>) =>
            of(
              IdeasActions.ideaListLoadedFailure({
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

export const loadIdeaPitches = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(IdeasActions.loadIdeaPitchList),
      switchMap((a) => {
        pitchesService.refreshIdeaPitches(a.args);
        return pitchesService.currentIdeaPitchListResult$.pipe(
          map((pitchListResult) =>
            IdeasActions.ideaPitchListLoadedSuccess({
              pitches: pitchListResult.data.allPitches.items || [],
            })
          ),
          catchError((result: ApolloQueryResult<AllPitchesCollectionSegment>) =>
            of(
              IdeasActions.ideaPitchListLoadedFailure({
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

export const loadIdea = createEffect(
  (actions$ = inject(Actions), ideasService = inject(IdeasService)) => {
    return actions$.pipe(
      ofType(IdeasActions.loadIdea),
      switchMap(({ ideaId }) => {
        return ideasService.getIdea(ideaId).pipe(
          map((ideaResult) =>
            IdeasActions.ideaLoadedSuccess({
              idea: ideaResult.data.idea,
            })
          ),
          catchError((result: ApolloQueryResult<{ idea: Idea }>) =>
            of(
              IdeasActions.ideaLoadedFailure({
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

export const addIdea = createEffect(
  (actions$ = inject(Actions), ideasService = inject(IdeasService)) => {
    return actions$.pipe(
      ofType(IdeasActions.addIdea),
      switchMap((addIdea) => {
        return ideasService.addIdea(addIdea).pipe(
          map((addIdeaResult) =>
            IdeasActions.ideaAddedSuccess({
              payload: <IdeaAddedPayload>addIdeaResult.data?.addIdea,
              args: <QueryAllIdeasArgs>{
                skip: 0,
                sortBy: 'name',
                take: 10,
              },
              date: new Date(),
            })
          ),
          catchError((result: MutationResult<{ addIdea: IdeaAddedPayload }>) =>
            of(
              IdeasActions.ideaAddedFailure({
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

export const removeIdea = createEffect(
  (actions$ = inject(Actions), ideasService = inject(IdeasService)) => {
    return actions$.pipe(
      ofType(IdeasActions.removeIdea),
      switchMap((removeIdea) => {
        return ideasService.removeIdea(removeIdea).pipe(
          map((removeIdeaResult) =>
            IdeasActions.ideaRemovedSuccess({
              payload: <string>removeIdeaResult.data?.removeIdea,
            })
          ),
          catchError((result: MutationResult<{ removeIdea: string }>) =>
            of(
              IdeasActions.ideaRemovedFailure({
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

export const modifyIdea = createEffect(
  (actions$ = inject(Actions), ideasService = inject(IdeasService)) => {
    return actions$.pipe(
      ofType(IdeasActions.modifyIdea),
      switchMap((modifyIdea) => {
        return ideasService.modifyIdea(modifyIdea).pipe(
          map((modifyIdeaResult) =>
            IdeasActions.ideaModifiedSuccess({
              payload: <string>modifyIdeaResult.data,
              newName: modifyIdea.newName,
              newDescription: modifyIdea.newDescription
            })
          ),
          catchError((result: MutationResult<{ modifyIdea: string }>) =>
            of(
              IdeasActions.ideaModifiedFailure({
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

export const redirectToIdea = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(IdeasActions.ideaAddedSuccess),
      tap((result) => {
        router.navigate(['/ideas', result.payload.ideaId]);
      })
    );
  },
  { functional: true, dispatch: false }
);

export const redirectToIdeas = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(IdeasActions.ideaRemovedSuccess),
      tap(() => {
        router.navigate(['/ideas']);
      })
    );
  },
  { functional: true, dispatch: false }
);
