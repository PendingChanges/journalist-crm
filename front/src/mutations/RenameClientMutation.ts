import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface RenameClientInput {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class RenameClientMutation extends Mutation<RenameClientInput> {
  override document = gql`
    mutation renameClient($id: String!, $name: String!) {
      renameClient(renameClientInput: { id: $id, name: $name })
    }
  `;
}
