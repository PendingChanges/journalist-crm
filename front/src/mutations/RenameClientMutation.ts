import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { RenameClientInput } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class RenameClientMutation extends Mutation<RenameClientInput> {
  override document = gql`
    mutation renameClient($id: String!, $name: String!) {
      renameClient(renameClient: { id: $id, name: $name })
    }
  `;
}
