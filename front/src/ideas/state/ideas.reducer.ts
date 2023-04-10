import { createReducer, on } from '@ngrx/store';
import { IdeasActions } from './ideas.actions';
import { IdeaState } from './IdeaState';

export const initialState: IdeaState = {
  ideas: [],
  errors: [],
  currentIdea: null,
  loading: false,
};

export const ideasReducer = createReducer(
  initialState,
  on(IdeasActions.loadIdeaList, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(IdeasActions.addIdea, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(IdeasActions.removeIdea, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(IdeasActions.modifyIdea, (state, _a) => {
    return {
      ...state,
      loading: true,
    };
  }),
  on(IdeasActions.ideaListLoadedSuccess, (state, result) => {
    return <IdeaState>{
      ...state,
      ideas: result.ideas,
      errors: [],
      loading: false,
    };
  }),
  on(IdeasActions.ideaListLoadedFailure, (state, result) => {
    return <IdeaState>{
      ...state,
      ideas: [],
      errors: result.errors,
      loading: false,
    };
  }),
  on(IdeasActions.ideaLoadedSuccess, (state, result) => {
    return <IdeaState>{
      ...state,
      currentIdea: result.idea,
      loading: false,
    };
  }),
  on(IdeasActions.ideaLoadedFailure, (state, result) => {
    return <IdeaState>{
      ...state,
      currentIdea: null,
      errors: result.errors,
      loading: false,
    };
  }),
  on(IdeasActions.ideaPitchListLoadedSuccess, (state, result) => {
    return <IdeaState>{
      ...state,
      currentIdea: {
        ...state.currentIdea,
        pitches: result.pitches,
      },
      loading: false,
    };
  }),
  on(IdeasActions.ideaPitchListLoadedFailure, (state, result) => {
    return <IdeaState>{
      ...state,
      currentIdea: {
        ...state.currentIdea,
        pitches: [],
      },
      errors: result.errors,
      loading: false,
    };
  }),
  on(IdeasActions.ideaRemovedSuccess, (state, removeIdea) => {
    return <IdeaState>{
      ...state,
      currentIdea: null,
      loading: false,
    };
  }),
  on(IdeasActions.ideaRemovedFailure, (state, result) => {
    return <IdeaState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  }),
  on(IdeasActions.ideaModifiedSuccess, (state, modifyIdea) => {
    return <IdeaState>{
      ...state,
      currentIdea: {
        ...state.currentIdea,
        name: modifyIdea.newName,
        description: modifyIdea.newDescription,
      },
      loading: false,
    };
  }),
  on(IdeasActions.ideaModifiedFailure, (state, result) => {
    return <IdeaState>{
      ...state,
      loading: false,
      errors: result.errors,
    };
  })
);
