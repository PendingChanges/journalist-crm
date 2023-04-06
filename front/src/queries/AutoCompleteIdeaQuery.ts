import { Injectable } from '@angular/core';
import { gql, Query } from 'apollo-angular';
import { Idea, QueryAutoCompleteIdeaArgs } from 'src/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AutoCompleteIdeaQuery extends Query<
  { autoCompleteIdea: Array<Idea> },
  QueryAutoCompleteIdeaArgs
> {
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
