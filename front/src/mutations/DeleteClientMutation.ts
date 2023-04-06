import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { DeleteClientInput } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class DeleteClientMutation extends Mutation<string, DeleteClientInput> {
  override document = gql`
    mutation removeClient($id: String!) {
      removeClient(deleteClient: { id: $id })
    }
  `;
}
