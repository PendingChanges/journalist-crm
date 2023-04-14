import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import {
  AllPitchesCollectionSegment,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllPitchesQuery extends Query<
  { allPitches: AllPitchesCollectionSegment },
  QueryAllPitchesArgs
> {
  override document = gql`
    query allPitches($clientId: String, $ideaId: String) {
      allPitches(clientId: $clientId, ideaId: $ideaId) {
        items {
          id
          content{
            title
            summary
          }
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
