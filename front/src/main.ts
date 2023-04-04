import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';


import { AppComponent } from './app/app.component';
import { TranslocoRootModule } from './app/transloco-root.module';
import { EditorModule } from '@tinymce/tinymce-angular';
import { withInterceptorsFromDi, provideHttpClient } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { provideAnimations } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app/app-routing.module';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { NgbDateAdapter, NgbDateNativeAdapter, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './app/auth.guard';
import { KeycloakService, KeycloakAngularModule } from 'keycloak-angular';
import { initializeKeycloak } from './app/initializeKeycloak';
import { APP_INITIALIZER, importProvidersFrom } from '@angular/core';
import { environment } from 'src/environments/environment';
import { InMemoryCache } from '@apollo/client/core';
import { HttpLink } from 'apollo-angular/http';
import { APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';


bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(BrowserModule, AppRoutingModule, ReactiveFormsModule, FormsModule, EditorModule, ApolloModule, TranslocoRootModule, KeycloakAngularModule, NgbModule),
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
        provideAnimations(),
        provideHttpClient(withInterceptorsFromDi())
    ]
})
  .catch(err => console.error(err));
