import { Pitch } from 'src/models/generated/graphql';

export type PitchState = {
  pitches: ReadonlyArray<Pitch>;
  errors: ReadonlyArray<string>;
  currentPitch: Pitch | null;
  loading: boolean;
};
