import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { MutationRemoveIdeaArgs } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class DeleteIdeaMutation extends Mutation<string, string> {
  override document = gql`
    mutation removeIdea($id: String!) {
      removeIdea(id: $id)
    }
  `;
}
