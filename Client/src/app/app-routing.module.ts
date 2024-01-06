import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersListComponent } from './members/members-list/members-list.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';

const routes: Routes = [

{
  path: '', runGuardsAndResolvers: 'always',
  canActivate: [authGuard],
  children: [
    {
      path: 'members', component: MembersListComponent
    },
    {
      path: 'members/:id', component: MemberDetailComponent
    },
    {
      path: 'lists', component: ListsComponent
    },
    {
      path: 'messages', component: MessagesComponent
    }

  ]}, 

  {
    path: '', component: HomeComponent
  },
  {
    path: 'error', component: TestErrorComponent
  },
  {
    path: 'not-found', component: NotFoundComponent
  },
 

{
  path: '**', component: HomeComponent, pathMatch: 'full'
}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
