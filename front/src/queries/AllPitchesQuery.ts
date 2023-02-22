import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Pitch } from 'src/models/Pitch';

export interface Response {
  clients: Pitch[];
}

@Injectable({
  providedIn: 'root',
})
export class AllPitchesQuery extends Query<Response> {
  override document = gql`
    query {
      allPitches {
        items {
          id
          title
          content
          deadLineDate
          issueDate
          statusCode
          ideas {
            items {
              id
              name
              description
            }
          }
          clients {
            items {
              id
              name
            }
          }
        }
      }
    }
  `;
}
