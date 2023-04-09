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
