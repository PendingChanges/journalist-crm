import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface IdeaInput {
  name: string;
  description: string | null;
}

export interface AddedIdeaPayload {
  ideaId: string;
}

@Injectable({
  providedIn: 'root',
})
export class AddIdeaMutation extends Mutation<AddedIdeaPayload, IdeaInput> {
  override document = gql`
    mutation addIdea($name: String!, $description: String) {
      addIdea(ideaInput: { name: $name, description: $description }) {
        ideaId
      }
    }
  `;
}
