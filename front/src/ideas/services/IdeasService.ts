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
import { ModifyIdeaMutation } from '../mutations/ModifyIdeaMutation';

@Injectable({
  providedIn: 'root',
})
export class IdeasService {
  constructor(
    private _allIdeasQuery: AllIdeasQuery,
    private _ideaQuery: IdeaQuery,
    private _addIdeaMutation: AddIdeaMutation,
    private _deleteIdeaMutation: DeleteIdeaMutation,
    private _autoCompleteIdeaQuery: AutoCompleteIdeaQuery,
    private _modifyIdeaMutation: ModifyIdeaMutation
  ) {}

  public getIdeas(args: QueryAllIdeasArgs) {
    return this._allIdeasQuery.fetch(args);
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
