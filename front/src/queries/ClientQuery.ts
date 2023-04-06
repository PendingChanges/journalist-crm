import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Client } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class ClientQuery extends Query<Response> {
  override document = gql`
    query client($id: String!) {
      client(id: $id) {
        id
        name
      }
    }
  `;
}
