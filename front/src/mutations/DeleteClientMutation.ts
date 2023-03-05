import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface RemoveClientInput {
  id: string;
}

@Injectable({
  providedIn: 'root',
})
export class DeleteClientMutation extends Mutation<{}, RemoveClientInput> {
  override document = gql`
    mutation removeClient($id: String!) {
      removeClient(id: $id)
    }
  `;
}
