import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Client, QueryAutoCompleteClientArgs } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AutoCompleteClientQuery extends Query<
  { autoCompleteClient: Array<Client> },
  QueryAutoCompleteClientArgs
> {
  override document = gql`
    query autoCompleteClient($text: String!) {
      autoCompleteClient(text: $text) {
        id
        name
      }
    }
  `;
}
