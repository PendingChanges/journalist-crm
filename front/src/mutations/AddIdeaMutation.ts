import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class AddIdeaMutation extends Mutation {
  override document = gql`
    mutation addIdea($name: String!, $description: String) {
      addIdea(ideaInput: { name: $name, description: $description }) {
        ideaId
      }
    }
  `;
}
