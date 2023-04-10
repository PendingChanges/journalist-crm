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
  ModifyIdeaInput,
} from 'src/models/generated/graphql';
import { AddIdeaMutation } from 'src/ideas/mutations/AddIdeaMutation';
import { CreateIdeaInput } from 'src/models/generated/graphql';
import { DeleteIdeaMutation } from 'src/ideas/mutations/DeleteIdeaMutation';
import { AllIdeasQuery } from 'src/ideas/queries/AllIdeasQuery';
import { AutoCompleteIdeaQuery } from 'src/ideas/queries/AutoCompleteIdeaQuery';
import { IdeaQuery } from 'src/ideas/queries/IdeaQuery';
import { ApolloQueryResult } from '@apollo/client/core';
import { ModifyIdeaMutation } from '../mutations/ModifyIdeaMutation';

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
    private _autoCompleteIdeaQuery: AutoCompleteIdeaQuery,
    private _modifyIdeaMutation: ModifyIdeaMutation
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

  public modifyIdea(
    value: ModifyIdeaInput
  ): Observable<MutationResult<string>> {
    return this._modifyIdeaMutation.mutate(value);
  }

  public autoComplete(text: string): Observable<Idea[]> {
    return this._autoCompleteIdeaQuery
      .fetch(<QueryAutoCompleteIdeaArgs>{ text: text })
      .pipe(map((result) => result.data.autoCompleteIdea));
  }
}
