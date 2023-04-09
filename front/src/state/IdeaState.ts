import { Idea } from 'src/generated/graphql';

export type IdeaState = {
  ideas: ReadonlyArray<Idea>;
  errors: ReadonlyArray<string>;
  currentIdea: Idea | null;
  loading: boolean;
};
