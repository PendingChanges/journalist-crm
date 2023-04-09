import { createActionGroup, props } from '@ngrx/store';
import {
  Pitch,
  PitchAddedPayload,
  CreatePitchInput,
  DeletePitchInput,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';
import { ErrorProps } from '../../common/state/ErrorProps';

export const PitchesActions = createActionGroup({
  source: 'Pitches',
  events: {
    'Add Pitch': props<CreatePitchInput>(),
    'Remove Pitch': props<DeletePitchInput>(),
    'Load Pitch': props<{ pitchId: string }>(),
    'Load Pitch List': props<{ args: QueryAllPitchesArgs; date?: Date }>(),
    'Pitch List Loaded Success': props<{ pitches: ReadonlyArray<Pitch> }>(),
    'Pitch List Loaded Failure': props<ErrorProps>(),
    'Pitch Loaded Success': props<{ pitch: Pitch }>(),
    'Pitch Loaded Failure': props<ErrorProps>(),
    'Pitch Added Success': props<{
      payload: PitchAddedPayload;
      args: QueryAllPitchesArgs;
      date?: Date;
    }>(),
    'Pitch Added Failure': props<ErrorProps>(),
    'Pitch Removed Success': props<{ payload: string }>(),
    'Pitch Removed Failure': props<ErrorProps>(),
  },
});
