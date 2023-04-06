import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { ClientAddedPayload, CreateClientInput } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddClientMutation extends Mutation<
  ClientAddedPayload,
  CreateClientInput
> {
  override document = gql`
    mutation addClient($name: String!) {
      addClient(clientInput: { name: $name }) {
        clientId
      }
    }
  `;
}
