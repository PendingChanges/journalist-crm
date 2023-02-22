import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class DeleteClientMutation extends Mutation {
  override document = gql`
    mutation removeClient($id: String!) {
      removeClient(id: $id)
    }
  `;
}
