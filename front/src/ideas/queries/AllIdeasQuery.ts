import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import {
  AllIdeasCollectionSegment,
  QueryAllIdeasArgs,
} from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllIdeasQuery extends Query<
  { allIdeas: AllIdeasCollectionSegment },
  QueryAllIdeasArgs
> {
  override document = gql`
    query ($skip: Int, $take: Int, $sortBy: String, $sortDirection: String) {
      allIdeas(
        skip: $skip
        take: $take
        sortBy: $sortBy
        sortDirection: $sortDirection
      ) {
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
