import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Idea } from 'src/models/Idea';

export interface Response {
  ideas: Idea[];
}

@Injectable({
  providedIn: 'root',
})
export class IdeaQuery extends Query<Response> {
  override document = gql`
    query idea($id: String!) {
      allIdeas(where: { id: { eq: $id } }) {
        id
        name
        description
        pitches {
          id
          title
          content
          deadLineDate
          issueDate
          status
          clients {
            id
            name
          }
        }
      }
    }
  `;
}
