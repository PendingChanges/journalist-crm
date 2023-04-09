import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Pitch } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class PitchQuery extends Query<Pitch, { id: string }> {
  override document = gql`
    query pitch($id: String!) {
      pitch(id: $id) {
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
  `;
}
