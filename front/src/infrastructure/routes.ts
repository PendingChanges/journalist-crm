import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ClientPageComponent } from '../clients/components/client-page/client-page.component';
import { ClientsComponent } from '../clients/components/clients-page/clients-page.component';
import { IdeaPageComponent } from '../ideas/components/idea-page/idea-page.component';
import { IdeasComponent } from '../ideas/components/ideas-page/ideas-page.component';
import { PitchPageComponent } from '../pitches/components/pitch-page/pitch-page.component';
import { PitchesPageComponent } from '../pitches/components/pitches-page/pitches-page.component';

export const ROUTES: Routes = [
  {
    path: 'clients',
    component: ClientsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'clients/:id',
    component: ClientPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'ideas',
    component: IdeasComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'ideas/:id',
    component: IdeaPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'pitches',
    component: PitchesPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'pitches/:id',
    component: PitchPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'clients',
  },
];
