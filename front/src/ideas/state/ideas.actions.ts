import { createActionGroup, props } from '@ngrx/store';
import {
  Idea,
  IdeaAddedPayload,
  CreateIdeaInput,
  DeleteIdeaInput,
  QueryAllIdeasArgs,
  QueryAllPitchesArgs,
  Pitch,
  ModifyIdeaInput,
} from 'src/models/generated/graphql';
import { ErrorProps } from '../../common/state/ErrorProps';

export const IdeasActions = createActionGroup({
  source: 'Ideas',
  events: {
    'Add Idea': props<CreateIdeaInput>(),
    'Remove Idea': props<DeleteIdeaInput>(),
    'Modify Idea': props<ModifyIdeaInput>(),
    'Load Idea': props<{ ideaId: string }>(),
    'Load Idea List': props<{ args: QueryAllIdeasArgs; append: boolean }>(),
    'Idea List Loaded Success': props<{
      ideas: ReadonlyArray<Idea>;
      append: boolean;
    }>(),
    'Idea List Loaded Failure': props<ErrorProps>(),
    'Load Idea Pitch List': props<{ args: QueryAllPitchesArgs }>(),
    'Idea Pitch List Loaded Success': props<{
      pitches: ReadonlyArray<Pitch>;
    }>(),
    'Idea Pitch List Loaded Failure': props<ErrorProps>(),
    'Idea Loaded Success': props<{ idea: Idea }>(),
    'Idea Loaded Failure': props<ErrorProps>(),
    'Idea Added Success': props<{
      payload: IdeaAddedPayload;
      args: QueryAllIdeasArgs;
      date?: Date;
    }>(),
    'Idea Added Failure': props<ErrorProps>(),
    'Idea Removed Success': props<{ payload: string }>(),
    'Idea Removed Failure': props<ErrorProps>(),
    'Idea Modified Success': props<{
      payload: string;
      newName: string;
      newDescription?: string | null;
    }>(),
    'Idea Modified Failure': props<ErrorProps>(),
  },
});
