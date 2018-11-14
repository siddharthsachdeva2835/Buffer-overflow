import { User } from './../models/user.model';
import { UserService } from './../services/user.service';
import { QuestionService } from './../services/question.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Question } from '../models/question.model';

@Component({
  selector: 'app-question-block',
  templateUrl: './question-block.component.html',
  styleUrls: ['./question-block.component.css']
})
export class QuestionBlockComponent implements OnInit {
  user = new User();
  self = false;
  constructor(private questionService: QuestionService,
                private userService: UserService) { }

  @Input() question: Question;
  @Input() check: boolean;
  @Output() delete: EventEmitter<any> = new EventEmitter();

  deleteQuestion() {
    this.delete.emit(this.question);
  }

  ngOnInit() {
    this.userService.currentUser.subscribe(data => {
      this.user = data;
      this.self = (data.userID === this.question.author.userID);
    });
  }

}
