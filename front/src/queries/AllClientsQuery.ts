import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class AllClientsQuery extends Query<Response> {
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
