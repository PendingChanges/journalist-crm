import { Idea } from 'src/models/generated/graphql';

export type IdeaState = {
  ideas: ReadonlyArray<Idea>;
  errors: ReadonlyArray<string>;
  currentIdea: Idea | null;
  loading: boolean;
};
