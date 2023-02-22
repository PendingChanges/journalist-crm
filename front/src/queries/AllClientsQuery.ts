import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Client } from 'src/models/Client';

export interface Response {
  clients: Client[];
}

@Injectable({
  providedIn: 'root',
})
export class AllClientsQuery extends Query<Response> {
  override document = gql`
    query {
      allClients {
        items{
          id
          name
          nbOfPitches
        }
      }
    }
  `;
}
