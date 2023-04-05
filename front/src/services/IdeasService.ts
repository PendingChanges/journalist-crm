import { Injectable } from '@angular/core';
import { QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import { Idea } from 'src/generated/graphql';
import { AddIdeaMutation, IdeaInput } from 'src/mutations/AddIdeaMutation';
import { DeleteIdeaMutation } from 'src/mutations/DeleteIdeaMutation';
import { AllIdeasQuery } from 'src/queries/AllIdeasQuery';
import { AutoCompleteIdeaQuery } from 'src/queries/AutoCompleteIdeaQuery';
import { IdeaQuery } from 'src/queries/IdeaQuery';

@Injectable({
  providedIn: 'root',
})
export class IdeasService {
  constructor(
    private _allIdeasQuery: AllIdeasQuery,
    private _addIdeaMutation: AddIdeaMutation,
    private _deleteIdeaMutation: DeleteIdeaMutation,
    private _autoCompleteIdeaQuery: AutoCompleteIdeaQuery,
    private _ideaQuery: IdeaQuery
  ) {}

  private _allIdeasQueryRef: QueryRef<any> | null = null;

  public get ideas$() {
    if (!this._allIdeasQueryRef) {
      this._allIdeasQueryRef = this._allIdeasQuery.watch();
    }

    return this._allIdeasQueryRef.valueChanges.pipe(
      map((result: any) => result.data.allIdeas.items)
    );
  }

  public refreshIdeas(): void {
    this._allIdeasQueryRef?.refetch();
  }

  public getIdea(id: string) {
    return this._ideaQuery
      .watch({
        id: id,
      })
      .valueChanges.pipe(map((result: any) => result.data.idea));
  }

  public addIdea(value: IdeaInput) {
    this._addIdeaMutation.mutate(value).subscribe(() => {
      this.refreshIdeas();
    });
  }

  public deleteIdea(id: string) {
    this._deleteIdeaMutation
      .mutate({
        id: id,
      })
      .subscribe(() => {
        this.refreshIdeas();
      });
  }

  public autoComplete(text: string): Observable<Idea[]> {
    return this._autoCompleteIdeaQuery
      .fetch({ text: text })
      .pipe(map((result) => result.data.autoCompleteIdea));
  }
}
