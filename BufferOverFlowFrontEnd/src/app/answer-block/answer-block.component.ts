import { UserService } from './../services/user.service';
import { AnswerService } from './../services/answer.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Answer } from '../models/answer.model';
import { User } from '../models/user.model';

@Component({
  selector: 'app-answer-block',
  templateUrl: './answer-block.component.html',
  styleUrls: ['./answer-block.component.css']
})
export class AnswerBlockComponent implements OnInit {
  inputValue = '';
  isAuth = false;
  upCount = 0;
  downCount = 0;
  netCount = 0;
  constructor(private answerService: AnswerService,
                private userService: UserService) { }
  @Input() answer: Answer;
  @Output() delete: EventEmitter<any> = new EventEmitter();
  status: boolean = null;
  user = new User();
  self = false;

  deleteAnswer() {
    this.delete.emit(this.answer);
  }

  upVote() {
    this.answerService.upVote(this.answer.questionID, this.answer.answerID).subscribe();

    if (this.status === true) {
      this.upCount--;
      this.status = null;
    } else if (this.status === false) {
      this.downCount--;
      this.upCount++;
      this.status = true;
    } else {
      this.upCount++;
      this.status = true;
    }
    this.netCount = this.upCount - this.downCount;
  }

  downVote () {
    this.answerService.downVote(this.answer.questionID, this.answer.answerID).subscribe();
    if (this.status === false) {
      this.status = null;
      this.downCount--;
    } else if (this.status === true) {
      this.upCount--;
      this.status = false;
      this.downCount++;
    } else {
      this.status = false;
      this.downCount++;
    }
    this.netCount = this.upCount - this.downCount;
  }

  updateAnswer(answerID: number) {
    this.answerService.updateAnswer(this.answer.questionID, answerID, this.inputValue).subscribe((data: Answer) => {
      this.answer.body = data.body;
    });
  }

  ngOnInit() {
    this.inputValue = this.answer.body;

    for (let i = 0 ; i < this.answer.votings.length ; i++) {
      if (this.answer.votings[i].status === true) {
        this.upCount++;
      } else {
        this.downCount++;
      }
    }


    this.netCount = this.upCount - this.downCount;

    this.userService.currentUser.subscribe(data => {
      this.user = data;
      this.self = (data.userID === this.answer.author.userID);
      if (this.answer.votings.length > 0) {
        for (let i = 0 ; i < this.answer.votings.length ; i++) {
          if ( this.answer.votings[i].userID === data.userID) {
            this.status = this.answer.votings[i].status;
          }
        }
      }
    });
    this.userService.isAuthenticated.subscribe(data => {
      this.isAuth = data;
    });
  }

}
