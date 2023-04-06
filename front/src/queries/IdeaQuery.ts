import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Idea } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class IdeaQuery extends Query<Response> {
  override document = gql`
    query idea($id: String!) {
      idea(id: $id) {
        id
        name
        description
      }
    }
  `;
}
