import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface PitchInput {
  title: string;
  content: string | null;
  deadLineDate: Date | null;
  issueDate: Date | null;
  clientId: string;
  ideaId: string;
}

export interface PitchAddedPayload {
  pitchId: string;
}

@Injectable({
  providedIn: 'root',
})
export class AddPitchMutation extends Mutation<PitchAddedPayload, PitchInput> {
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
        pitchInput: {
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
