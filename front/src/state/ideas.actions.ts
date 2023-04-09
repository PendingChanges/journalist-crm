import { createActionGroup, props } from '@ngrx/store';
import {
  Idea,
  IdeaAddedPayload,
  CreateIdeaInput,
  DeleteIdeaInput,
  QueryAllIdeasArgs,
} from 'src/generated/graphql';
import { ErrorProps } from './ErrorProps';

export const IdeasActions = createActionGroup({
  source: 'Ideas',
  events: {
    'Add Idea': props<CreateIdeaInput>(),
    'Remove Idea': props<DeleteIdeaInput>(),
    'Load Idea': props<{ ideaId: string }>(),
    'Load Idea List': props<{ args: QueryAllIdeasArgs; date?: Date }>(),
    'Idea List Loaded Success': props<{ ideas: ReadonlyArray<Idea> }>(),
    'Idea List Loaded Failure': props<ErrorProps>(),
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
  },
});