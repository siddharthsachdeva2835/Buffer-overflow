import { AuthGuard } from './services/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { LoginComponent } from './auth.shared/login/login.component';
import { SignupComponent } from './auth.shared/signup/signup.component';
import { HeaderComponent } from './layout.shared/header/header.component';
import { FooterComponent } from './layout.shared/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NoAuthGuard } from './services/no-auth-guard.service';
import { HttpClientModule } from '@angular/common/http';
import { QuestionBlockComponent } from './question-block/question-block.component';
import { QuestionComponent } from './quesion/question.component';
import { AnswerBlockComponent } from './answer-block/answer-block.component';
import { AddQuestionComponent } from './add-question/add-question.component';
import { UpdateQuestionComponent } from './update-question/update-question.component';
import {NgxPaginationModule} from 'ngx-pagination';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    LoginComponent,
    SignupComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    QuestionBlockComponent,
    QuestionComponent,
    AnswerBlockComponent,
    AddQuestionComponent,
    UpdateQuestionComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule
  ],
  providers: [AuthGuard, NoAuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
