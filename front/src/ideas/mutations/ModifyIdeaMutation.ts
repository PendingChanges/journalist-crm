import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { ModifyIdeaInput } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class ModifyIdeaMutation extends Mutation<string, ModifyIdeaInput> {
  override document = gql`
    mutation modifyIdea(
      $id: String!
      $newName: String!
      $newDescription: String
    ) {
      modifyIdea(
        modifyIdea: {
          id: $id
          newName: $newName
          newDescription: $newDescription
        }
      )
    }
  `;
}
