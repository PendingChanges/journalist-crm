import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Idea } from 'src/models/Idea';

export interface Response {
  ideas: Idea[];
}

@Injectable({
  providedIn: 'root',
})
export class AllIdeasQuery extends Query<Response> {
  override document = gql`
    query {
      allIdeas {
        items {
          id
          name
          description
          nbOfPitches
        }
      }
    }
  `;
}
