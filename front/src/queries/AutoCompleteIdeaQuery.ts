import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Idea } from 'src/generated/graphql';

export interface Response {
  autoCompleteIdea: Idea[];
}

export interface AutoCompleteInput {
  text: string;
}

@Injectable({
  providedIn: 'root',
})
export class AutoCompleteIdeaQuery extends Query<Response, AutoCompleteInput> {
  override document = gql`
    query autoCompleteIdea($text: String!) {
      autoCompleteIdea(text: $text) {
        id
        name
        description
      }
    }
  `;
}
