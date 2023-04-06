import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { AllPitchesCollectionSegment, QueryAllPitchesArgs } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllPitchesQuery extends Query<
  AllPitchesCollectionSegment,
  QueryAllPitchesArgs
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
