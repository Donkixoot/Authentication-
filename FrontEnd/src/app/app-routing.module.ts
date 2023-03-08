import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/navbar/login/login.component';
import { RegisterComponent } from './components/navbar/register/register.component';
import { PostListComponent } from './components/post/post-list/post-list.component';
import { PostComponent } from './components/post/post.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/postlist', pathMatch: 'full' },
  { path: 'postlist', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'post/:id', component: PostComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
