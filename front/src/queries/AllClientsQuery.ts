import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import {
  AllClientsCollectionSegment,
  QueryAllClientsArgs,
} from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AllClientsQuery extends Query<
  { allClients: AllClientsCollectionSegment },
  QueryAllClientsArgs
> {
  override document = gql`
    query {
      allClients {
        items {
          id
          name
          nbOfPitches
        }
      }
    }
  `;
}
