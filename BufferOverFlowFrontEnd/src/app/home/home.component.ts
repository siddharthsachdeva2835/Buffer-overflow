import { FormGroup, FormControl } from '@angular/forms';
import { Question } from './../models/question.model';
import { QuestionService } from '../services/question.service';
import { UserService } from '../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { TagsService } from '../services/tags.service';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import {NgxPaginationModule} from 'ngx-pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  p = 1;
  check = false;
  questions = new Array<Question>();
  date: Date;
  dateString: string;
  isAuth: boolean;

  constructor(private userService: UserService, private questionService: QuestionService ,
                      private router: Router, private activatedRoute: ActivatedRoute) {
  }

  searchForm = new FormGroup({
    body: new FormControl('')
  });

  onSubmit() {
    this.questionService.getAllQuesionsBySearchString(this.searchForm.value.body).subscribe(data => {
      this.questions = data;
      this.searchForm.reset();
    });
  }

  deleteQuestion(question: Question) {
    this.questions.splice(this.questions.indexOf(question), 1);
    this.questionService.deleteQuestion(question.questionID).subscribe();
  }

  ngOnInit() {
    this.userService.isAuthenticated.subscribe(data => this.isAuth = data);
    this.questionService.getAllQuesions().subscribe(data => {
      console.log(data);
      this.questions = data;
    });
  }
}
