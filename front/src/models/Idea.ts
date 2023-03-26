import { Pitch } from './Pitch';

export interface Idea {
  id: string;
  name: string;
  description: string | null;
  nbOfPitches: number;
}
