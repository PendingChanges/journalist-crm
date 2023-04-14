import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import {
  ModifyIdeaInput,
  ModifyPitchInput,
} from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class ModifyPitchMutation extends Mutation<string, ModifyPitchInput> {
  override document = gql`
    mutation modifyPitch(
      $id: String!
      $content: PitchContentInput!
      $deadLineDate: DateTime
      $issueDate: DateTime
      $clientId: String!
      $ideaId: String!
    ) {
      modifyPitch(
        modifyPitch: {
          id: $id
          content: $content
          deadLineDate: $deadLineDate
          issueDate: $issueDate
          clientId: $clientId
          ideaId: $ideaId
        }
      )
    }
  `;
}
