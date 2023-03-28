import { Client } from './Client';
import { Idea } from './Idea';

export interface Pitch {
  id: string;
  title: string;
  content: string;
  idea: Idea;
  client: Client;
}
