import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import {
  AllClientsCollectionSegment,
  QueryAllClientsArgs,
} from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllClientsQuery extends Query<
  { allClients: AllClientsCollectionSegment },
  QueryAllClientsArgs
> {
  override document = gql`
    query allClients(
      $skip: Int
      $take: Int
      $sortBy: String
      $sortDirection: String
    ) {
      allClients (skip: $skip, take: $take, sortBy: $sortBy, sortDirection: $sortDirection ){
        items {
          id
          name
          nbOfPitches
        }
      }
    }
  `;
}
