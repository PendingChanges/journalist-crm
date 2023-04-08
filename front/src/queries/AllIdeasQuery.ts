import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { AllIdeasCollectionSegment, QueryAllIdeasArgs } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllIdeasQuery extends Query<AllIdeasCollectionSegment, QueryAllIdeasArgs> {
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
