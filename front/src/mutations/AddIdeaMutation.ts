import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { IdeaAddedPayload, IdeaInput } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddIdeaMutation extends Mutation<
  IdeaAddedPayload,
  IdeaInput
> {
  override document = gql`
    mutation addIdea($name: String!, $description: String) {
      addIdea(ideaInput: { name: $name, description: $description }) {
        ideaId
      }
    }
  `;
}
