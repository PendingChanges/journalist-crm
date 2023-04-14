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
    data: { animation: 0 },
  },
  {
    path: 'clients/:id',
    component: ClientPageComponent,
    canActivate: [AuthGuard],
    data: { animation: 'right' },
  },
  {
    path: 'ideas',
    component: IdeasComponent,
    canActivate: [AuthGuard],
    data: { animation: 1 },
  },
  {
    path: 'ideas/:id',
    component: IdeaPageComponent,
    canActivate: [AuthGuard],
    data: { animation: 'right' },
  },
  {
    path: 'pitches',
    component: PitchesPageComponent,
    canActivate: [AuthGuard],
    data: { animation: 2 },
  },
  {
    path: 'pitches/:id',
    component: PitchPageComponent,
    canActivate: [AuthGuard],
    data: { animation: 'right' },
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'clients',
  },
];
