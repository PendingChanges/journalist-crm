import { createSelector, createFeatureSelector } from '@ngrx/store';
import { IdeaState } from './IdeaState';

export const selectIdeaState = createFeatureSelector<IdeaState>('ideaState');

export const selectIdeas = createSelector(
  selectIdeaState,
  (ideaState: IdeaState) => {
    return ideaState.ideas;
  }
);

export const currentIdea = createSelector(
  selectIdeaState,
  (ideaState: IdeaState) => {
    return ideaState.currentIdea;
  }
);

export const currentIdeaPitches = createSelector(currentIdea, (idea) => {
  return idea?.pitches || [];
});

export const loading = createSelector(
  selectIdeaState,
  (ideaState: IdeaState) => {
    return ideaState.loading;
  }
);
