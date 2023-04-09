import { createSelector, createFeatureSelector } from '@ngrx/store';
import { PitchState } from './PitchState';

export const selectPitchState =
  createFeatureSelector<PitchState>('pitchState');

export const selectPitches = createSelector(
  selectPitchState,
  (pitchState: PitchState) => {
    return pitchState.pitches;
  }
);

export const currentPitch = createSelector(
  selectPitchState,
  (pitchState: PitchState) => {
    return pitchState.currentPitch;
  }
);

export const loading = createSelector(
  selectPitchState,
  (pitchState: PitchState) => {
    return pitchState.loading;
  }
);
