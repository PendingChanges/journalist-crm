import { createReducer, on } from '@ngrx/store';
import { PitchesActions } from './pitches.actions';
import { PitchState } from './PitchState';

export const initialState: PitchState = {
  pitches: [],
  errors: [],
  currentPitch: null,
  loading: false,
};

export const pitchesReducer = createReducer(
  initialState,
  on(PitchesActions.loadPitchList, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(PitchesActions.addPitch, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(PitchesActions.removePitch, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(PitchesActions.pitchListLoadedSuccess, (state, result) => {
    return <PitchState>{
      ...state,
      pitches: result.pitches,
      errors: [],
      loading: false,
    };
  }),
  on(PitchesActions.pitchListLoadedFailure, (state, result) => {
    return <PitchState>{
      ...state,
      pitches: [],
      errors: result.errors,
      loading: false,
    };
  }),
  on(PitchesActions.pitchLoadedSuccess, (state, result) => {
    return <PitchState>{
      ...state,
      currentPitch: result.pitch,
      loading: false,
    };
  }),
  on(PitchesActions.pitchLoadedFailure, (state, result) => {
    return <PitchState>{
      ...state,
      currentPitch: null,
      errors: result.errors,
      loading: false,
    };
  }),
  on(PitchesActions.pitchRemovedSuccess, (state, removePitch) => {
    return <PitchState>{
      ...state,
      currentPitch: null,
      loading: false,
    };
  }),
  on(PitchesActions.pitchRemovedFailure, (state, result) => {
    return <PitchState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  })
);
