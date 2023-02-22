import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class DeleteIdeaMutation extends Mutation {
  override document = gql`
    mutation removeIdea($id: String!) {
      removeIdea(id: $id)
    }
  `;
}
