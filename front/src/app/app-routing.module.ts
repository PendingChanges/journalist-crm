import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ClientPageComponent } from './clients/client-page/client-page.component';
import { ClientsComponent } from './clients/clients-page/clients-page.component';
import { IdeaPageComponent } from './ideas/idea-page/idea-page.component';
import { IdeasComponent } from './ideas/ideas-page/ideas-page.component';
import { PitchPageComponent } from './pitches/pitch-page/pitch-page.component';
import { PitchesPageComponent } from './pitches/pitches-page/pitches-page.component';

const routes: Routes = [
  {
    path: 'clients',
    component: ClientsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'clients/:id',
    component: ClientPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'ideas',
    component: IdeasComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'ideas/:id',
    component: IdeaPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'pitches',
    component: PitchesPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'pitches/:id',
    component: PitchPageComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'clients'
  },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
