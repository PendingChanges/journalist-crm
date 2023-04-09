import { Idea, Pitch } from 'src/models/generated/graphql';

export type IdeaState = {
  ideas: ReadonlyArray<Idea>;
  errors: ReadonlyArray<string>;
  currentIdea: (Idea & { pitches: ReadonlyArray<Pitch> }) | null;
  loading: boolean;
};
