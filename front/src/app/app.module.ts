import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { EditorModule } from '@tinymce/tinymce-angular';

import { NavbarComponent } from './layout/navbar/navbar.component';
import { ClientsComponent } from './clients/clients-page/clients-page.component';
import { IdeasComponent } from './ideas/ideas-page/ideas-page.component';
import { AddIdeaComponent } from './ideas/add-idea/add-idea.component';
import { IdeaListComponent } from './ideas/idea-list/idea-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClientListComponent } from './clients/client-list/client-list.component';
import { AddClientComponent } from './clients/add-client/add-client.component';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';
import { ClientPageComponent } from './clients/client-page/client-page.component';
import { ClientActionMenuComponent } from './clients/client-action-menu/client-action-menu.component';
import { ClientDeleteButtonComponent } from './clients/client-delete-button/client-delete-button.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { AddPitchButtonComponent } from './pitches/add-pitch/add-pitch-button/add-pitch-button.component';
import { PitchListComponent } from './pitches/pitch-list/pitch-list.component';
import { TranslocoRootModule } from './transloco-root.module';
import { LanguagePickerComponent } from './layout/language-picker/language-picker.component';
import { PitchesPageComponent } from './pitches/pitches-page/pitches-page.component';
import { PitchPageComponent } from './pitches/pitch-page/pitch-page.component';
import { IdeaPageComponent } from './ideas/idea-page/idea-page.component';
import { ClientsActionMenuComponent } from './clients/clients-action-menu/clients-action-menu.component';
import { AddClientButtonComponent } from './clients/add-client-button/add-client-button.component';
import { IdeasActionMenuComponent } from './ideas/ideas-action-menu/ideas-action-menu.component';
import { AddIdeaButtonComponent } from './ideas/add-idea-button/add-idea-button.component';
import { PitchesActionMenuComponent } from './pitches/pitches-action-menu/pitches-action-menu.component';
import { IdeaActionMenuComponent } from './ideas/idea-action-menu/idea-action-menu.component';
import { AddPitchComponent } from './pitches/add-pitch/add-pitch.component';
import { DeleteIdeaButtonComponent } from './ideas/delete-idea-button/delete-idea-button.component';
import { initializeKeycloak } from './initializeKeycloak';
import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';
import { AuthGuard } from './auth.guard';
import { environment } from 'src/environments/environment';
import { ClientSelectorComponent } from './clients/client-selector/client-selector.component';
import {
  NgbDateAdapter,
  NgbDateNativeAdapter,
  NgbModule,
} from '@ng-bootstrap/ng-bootstrap';
import { IdeaSelectorComponent } from './ideas/idea-selector/idea-selector.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ClientsComponent,
    IdeasComponent,
    AddIdeaComponent,
    IdeaListComponent,
    ClientListComponent,
    AddClientComponent,
    ClientPageComponent,
    ClientActionMenuComponent,
    ClientDeleteButtonComponent,
    ConfirmDialogComponent,
    AddPitchButtonComponent,
    PitchListComponent,
    LanguagePickerComponent,
    PitchesPageComponent,
    PitchPageComponent,
    IdeaPageComponent,
    ClientsActionMenuComponent,
    AddClientButtonComponent,
    IdeasActionMenuComponent,
    AddIdeaButtonComponent,
    PitchesActionMenuComponent,
    IdeaActionMenuComponent,
    AddPitchComponent,
    DeleteIdeaButtonComponent,
    ClientSelectorComponent,
    IdeaSelectorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    EditorModule,
    ApolloModule,
    TranslocoRootModule,
    KeycloakAngularModule,
    NgbModule,
  ],
  providers: [
    {
      provide: APOLLO_OPTIONS,
      useFactory(httpLink: HttpLink) {
        return {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: environment.graphqlUrl,
          }),
        };
      },
      deps: [HttpLink],
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      multi: true,
      deps: [KeycloakService],
    },
    AuthGuard,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
