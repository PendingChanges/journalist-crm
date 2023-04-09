import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { RenameClientInput } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class RenameClientMutation extends Mutation<string, RenameClientInput> {
  override document = gql`
    mutation renameClient($id: String!, $newName: String!) {
      renameClient(renameClient: { id: $id, newName: $newName })
    }
  `;
}
