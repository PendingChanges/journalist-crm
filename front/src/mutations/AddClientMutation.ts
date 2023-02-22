import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class AddClientMutation extends Mutation {
  override document = gql`
    mutation addClient($name: String!) {
      addClient(clientInput: { name: $name }) {
        clientId
      }
    }
  `;
}
