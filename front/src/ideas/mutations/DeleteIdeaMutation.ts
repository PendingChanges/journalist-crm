import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { DeleteIdeaInput } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class DeleteIdeaMutation extends Mutation<
  { removeIdea: string },
  DeleteIdeaInput
> {
  override document = gql`
    mutation removeIdea($id: String!) {
      removeIdea(deleteIdea: { id: $id })
    }
  `;
}
