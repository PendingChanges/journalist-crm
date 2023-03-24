import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Pitch } from 'src/models/Pitch';

export interface AllPitchedResponse {
  pitches: Pitch[];
}

export interface AllPitchesInput {
  clientId: string | null;
  ideaId: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class AllPitchesQuery extends Query<
  AllPitchedResponse,
  AllPitchesInput
> {
  override document = gql`
    query allPitches($clientId: String, $ideaId: String) {
      allPitches(clientId: $clientId, ideaId: $ideaId) {
        items {
          id
          title
          content
          deadLineDate
          issueDate
          idea {
            id
            name
            description
          }
          client {
            id
            name
          }
        }
      }
    }
  `;
}
