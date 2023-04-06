import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import {
  MutationAddPitchArgs,
  PitchAddedPayload,
  CreatePitchInput,
} from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddPitchMutation extends Mutation<PitchAddedPayload, CreatePitchInput> {
  override document = gql`
    mutation addPitch(
      $title: String!
      $content: String
      $deadLineDate: DateTime
      $issueDate: DateTime
      $clientId: String!
      $ideaId: String!
    ) {
      addPitch(
        createPitch: {
          title: $title
          content: $content
          deadLineDate: $deadLineDate
          issueDate: $issueDate
          clientId: $clientId
          ideaId: $ideaId
        }
      ) {
        pitchId
      }
    }
  `;
}
