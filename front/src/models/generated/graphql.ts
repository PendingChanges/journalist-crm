export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  DateTime: any;
};

/** A segment of a collection. */
export type AllClientsCollectionSegment = {
  __typename?: 'AllClientsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Client>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int'];
};

/** A segment of a collection. */
export type AllIdeasCollectionSegment = {
  __typename?: 'AllIdeasCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Idea>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int'];
};

/** A segment of a collection. */
export type AllPitchesCollectionSegment = {
  __typename?: 'AllPitchesCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Pitch>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int'];
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER',
  Validation = 'VALIDATION'
}

export type Client = {
  __typename?: 'Client';
  id: Scalars['String'];
  name: Scalars['String'];
  nbOfPitches: Scalars['Int'];
  userId: Scalars['String'];
};

export type ClientAddedPayload = {
  __typename?: 'ClientAddedPayload';
  clientId?: Maybe<Scalars['String']>;
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
};

export type CreateClientInput = {
  name: Scalars['String'];
};

export type CreateIdeaInput = {
  description?: InputMaybe<Scalars['String']>;
  name: Scalars['String'];
};

export type CreatePitchInput = {
  clientId: Scalars['String'];
  content?: InputMaybe<Scalars['String']>;
  deadLineDate?: InputMaybe<Scalars['DateTime']>;
  ideaId: Scalars['String'];
  issueDate?: InputMaybe<Scalars['DateTime']>;
  title: Scalars['String'];
};

export type DeleteClientInput = {
  id: Scalars['String'];
};

export type DeleteIdeaInput = {
  id: Scalars['String'];
};

export type DeletePitchInput = {
  id: Scalars['String'];
};

export type DomainError = {
  __typename?: 'DomainError';
  domainErrors: Array<Error>;
  message: Scalars['String'];
};

export type Error = {
  __typename?: 'Error';
  code: Scalars['String'];
  label: Scalars['String'];
};

export type Idea = {
  __typename?: 'Idea';
  description?: Maybe<Scalars['String']>;
  id: Scalars['String'];
  name: Scalars['String'];
  nbOfPitches: Scalars['Int'];
  userId: Scalars['String'];
};

export type IdeaAddedPayload = {
  __typename?: 'IdeaAddedPayload';
  ideaId?: Maybe<Scalars['String']>;
};

export type Mutation = {
  __typename?: 'Mutation';
  addClient: ClientAddedPayload;
  addIdea: IdeaAddedPayload;
  addPitch: PitchAddedPayload;
  removeClient: Scalars['String'];
  removeIdea: Scalars['String'];
  removePitch: Scalars['String'];
  renameClient: Scalars['String'];
};


export type MutationAddClientArgs = {
  createClient: CreateClientInput;
};


export type MutationAddIdeaArgs = {
  createIdea: CreateIdeaInput;
};


export type MutationAddPitchArgs = {
  createPitch: CreatePitchInput;
};


export type MutationRemoveClientArgs = {
  deleteClient: DeleteClientInput;
};


export type MutationRemoveIdeaArgs = {
  deleteIdea: DeleteIdeaInput;
};


export type MutationRemovePitchArgs = {
  deletePitch: DeletePitchInput;
};


export type MutationRenameClientArgs = {
  renameClient: RenameClientInput;
};

export type Pitch = {
  __typename?: 'Pitch';
  client?: Maybe<Client>;
  clientId: Scalars['String'];
  content?: Maybe<Scalars['String']>;
  deadLineDate?: Maybe<Scalars['DateTime']>;
  id: Scalars['String'];
  idea?: Maybe<Idea>;
  ideaId: Scalars['String'];
  issueDate?: Maybe<Scalars['DateTime']>;
  title: Scalars['String'];
  userId: Scalars['String'];
};

export type PitchAddedPayload = {
  __typename?: 'PitchAddedPayload';
  pitchId?: Maybe<Scalars['String']>;
};

export type Query = {
  __typename?: 'Query';
  allClients?: Maybe<AllClientsCollectionSegment>;
  allIdeas?: Maybe<AllIdeasCollectionSegment>;
  allPitches?: Maybe<AllPitchesCollectionSegment>;
  autoCompleteClient: Array<Client>;
  autoCompleteIdea: Array<Idea>;
  client?: Maybe<Client>;
  idea?: Maybe<Idea>;
  pitch?: Maybe<Pitch>;
};


export type QueryAllClientsArgs = {
  skip?: InputMaybe<Scalars['Int']>;
  sortBy?: InputMaybe<Scalars['String']>;
  take?: InputMaybe<Scalars['Int']>;
};


export type QueryAllIdeasArgs = {
  skip?: InputMaybe<Scalars['Int']>;
  sortBy?: InputMaybe<Scalars['String']>;
  take?: InputMaybe<Scalars['Int']>;
};


export type QueryAllPitchesArgs = {
  clientId?: InputMaybe<Scalars['String']>;
  ideaId?: InputMaybe<Scalars['String']>;
  skip?: InputMaybe<Scalars['Int']>;
  sortBy?: InputMaybe<Scalars['String']>;
  take?: InputMaybe<Scalars['Int']>;
};


export type QueryAutoCompleteClientArgs = {
  text: Scalars['String'];
};


export type QueryAutoCompleteIdeaArgs = {
  text: Scalars['String'];
};


export type QueryClientArgs = {
  id: Scalars['String'];
};


export type QueryIdeaArgs = {
  id: Scalars['String'];
};


export type QueryPitchArgs = {
  id: Scalars['String'];
};

export type RenameClientInput = {
  id: Scalars['String'];
  newName: Scalars['String'];
};
