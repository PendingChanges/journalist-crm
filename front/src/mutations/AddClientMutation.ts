import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface ClientInput {
  name: string;
}

export interface AddedClientPayload {
  clientId: string;
}

@Injectable({
  providedIn: 'root',
})
export class AddClientMutation extends Mutation<
  AddedClientPayload,
  ClientInput
> {
  override document = gql`
    mutation addClient($name: String!) {
      addClient(clientInput: { name: $name }) {
        clientId
      }
    }
  `;
}
