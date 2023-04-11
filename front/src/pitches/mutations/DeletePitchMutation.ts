import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { DeletePitchInput } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class DeletePitchMutation extends Mutation<
  { removePitch: string },
  DeletePitchInput
> {
  override document = gql`
    mutation removePitch($id: String!) {
      removePitch(deletePitch: { id: $id })
    }
  `;
}
