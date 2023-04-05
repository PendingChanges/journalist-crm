import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Client } from 'src/generated/graphql';

export interface Response {
  autoCompleteClient: Client[];
}

export interface AutoCompleteInput {
  text: string;
}

@Injectable({
  providedIn: 'root',
})
export class AutoCompleteClientQuery extends Query<Response, AutoCompleteInput> {
  override document = gql`
    query autoCompleteClient($text: String!) {
      autoCompleteClient(text: $text) {
        id
        name
      }
    }
  `;
}
