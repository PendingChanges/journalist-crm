import { Injectable } from '@angular/core';
import { MutationResult, QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import {
  AllIdeasCollectionSegment,
  Idea,
  IdeaAddedPayload,
  DeleteIdeaInput,
  QueryAllIdeasArgs,
  QueryAutoCompleteIdeaArgs,
} from 'src/generated/graphql';
import { AddIdeaMutation } from 'src/mutations/AddIdeaMutation';
import { CreateIdeaInput } from 'src/generated/graphql';
import { DeleteIdeaMutation } from 'src/mutations/DeleteIdeaMutation';
import { AllIdeasQuery } from 'src/queries/AllIdeasQuery';
import { AutoCompleteIdeaQuery } from 'src/queries/AutoCompleteIdeaQuery';
import { IdeaQuery } from 'src/queries/IdeaQuery';
import { ApolloQueryResult } from '@apollo/client/core';

@Injectable({
  providedIn: 'root',
})
export class IdeasService {
  private _allIdeasQueryRef: QueryRef<
    { allIdeas: AllIdeasCollectionSegment },
    QueryAllIdeasArgs
  > | null = null;

  constructor(
    allIdeasQuery: AllIdeasQuery,
    private _ideaQuery: IdeaQuery,
    private _addIdeaMutation: AddIdeaMutation,
    private _deleteIdeaMutation: DeleteIdeaMutation,
    private _autoCompleteIdeaQuery: AutoCompleteIdeaQuery
  ) {
    this._allIdeasQueryRef = allIdeasQuery.watch();
    this.ideaListResult$ = this._allIdeasQueryRef.valueChanges;
  }

  public ideaListResult$: Observable<
    ApolloQueryResult<{ allIdeas: AllIdeasCollectionSegment }>
  >;

  public refreshIdeas(args: QueryAllIdeasArgs): void {
    this._allIdeasQueryRef?.refetch(args);
  }

  public getIdea(id: string) {
    return this._ideaQuery.watch({
      id: id,
    }).valueChanges;
  }

  public addIdea(
    value: CreateIdeaInput
  ): Observable<MutationResult<{ addIdea: IdeaAddedPayload }>> {
    return this._addIdeaMutation.mutate(value);
  }

  public removeIdea(
    deleteIdeaInput: DeleteIdeaInput
  ): Observable<MutationResult<{ removeIdea: string }>> {
    return this._deleteIdeaMutation.mutate(deleteIdeaInput);
  }

  public autoComplete(text: string): Observable<Idea[]> {
    return this._autoCompleteIdeaQuery
      .fetch(<QueryAutoCompleteIdeaArgs>{ text: text })
      .pipe(map((result) => result.data.autoCompleteIdea));
  }
}
