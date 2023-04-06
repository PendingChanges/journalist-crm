import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { IdeaAddedPayload, CreatePitchInput, CreateIdeaInput } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddIdeaMutation extends Mutation<
  IdeaAddedPayload,
  CreateIdeaInput
> {
  override document = gql`
    mutation addIdea($name: String!, $description: String) {
      addIdea(createIdea: { name: $name, description: $description }) {
        ideaId
      }
    }
  `;
}
