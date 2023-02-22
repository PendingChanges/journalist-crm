import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Client } from 'src/models/Client';

export interface Response {
  clients: Client[];
}

@Injectable({
  providedIn: 'root',
})
export class ClientQuery extends Query<Response> {
  override document = gql`
    query client($id: String!) {
      allClients(where: { id: { eq: $id } }) {
        id
        name
        pitches {
          id
          title
          content
          deadLineDate
          issueDate
          status
          ideas{
            id
            name
            description
          }
        }
      }
    }
  `;
}
