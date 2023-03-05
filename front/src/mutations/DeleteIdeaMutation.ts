import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

export interface RemoveIdeaInput {
  id: string;
}

@Injectable({
  providedIn: 'root',
})
export class DeleteIdeaMutation extends Mutation<{}, RemoveIdeaInput> {
  override document = gql`
    mutation removeIdea($id: String!) {
      removeIdea(id: $id)
    }
  `;
}
