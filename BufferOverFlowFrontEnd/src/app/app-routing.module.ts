import { UpdateQuestionComponent } from './update-question/update-question.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { SignupComponent } from './auth.shared/signup/signup.component';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule} from '@angular/router';
import { LoginComponent } from './auth.shared/login/login.component';
import { AuthGuard } from './services/auth-guard.service';
import { NoAuthGuard } from './services/no-auth-guard.service';
import { QuestionComponent } from './quesion/question.component';


const routes: Routes = [
  { path: '', redirectTo: '/home' , pathMatch: 'full'},
  { path: 'home', component: HomeComponent},
  { path: 'update/:id', component: UpdateQuestionComponent},
  { path: 'add-question', component: AddQuestionComponent, canActivate: [AuthGuard]},
  { path: 'question/:id', component: QuestionComponent },
  { path: 'auth', component: AuthComponent , children: [
    { path: 'login', component: LoginComponent, canActivate: [NoAuthGuard]},
    { path: 'signup', component: SignupComponent, canActivate: [NoAuthGuard]},
  ]},
  { path: '**' , redirectTo: '/home' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
