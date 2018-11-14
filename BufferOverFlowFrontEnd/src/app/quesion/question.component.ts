import { UserService } from './../services/user.service';
import { AnswerService } from '../services/answer.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionService } from '../services/question.service';
import { Question } from '../models/question.model';
import { Component, OnInit } from '@angular/core';
import { Answer } from '../models/answer.model';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  question = null;
  answers: Answer[];
  check = true;
  isAuth = false;
  constructor(private questionService: QuestionService,
                private activatedRoute: ActivatedRoute,
                  private answerService: AnswerService,
                    private router: Router,
                      private userService: UserService) { }

  addAnswerForm = new FormGroup({
    body: new FormControl('')
  });


  onSubmit() {
    console.log(this.addAnswerForm.value);

    this.answerService.addAnswer(this.question.questionID, this.addAnswerForm.value).subscribe(
      (ans: Answer) => {
        console.log(ans);
        this.question.answerCount++;
        this.answers.push(ans);
      }
    );
    this.addAnswerForm.reset();
  }

  deleteAnswer(answer: Answer) {
    this.answerService.deleteAnswer(answer.questionID, answer.answerID).subscribe();
    this.answers.splice(this.answers.indexOf(answer), 1);
    this.question.answerCount--;
  }

  ngOnInit() {
    debugger;
    this.questionService.getQuestion(this.activatedRoute.snapshot.paramMap.get('id'))
                .subscribe(data => {
                  this.question = data;
                  console.log(this.question);
                  this.answers = data.answers;
                  this.answers.sort( (a: Answer, b: Answer) => {
                    let countA = 0;
                    let countB = 0;

                    for (let i = 0 ; i < a.votings.length ; i++) {
                      if (a.votings[i].status === true ) {
                        countA++;
                      }
                    }

                    for (let i = 0 ; i < b.votings.length ; i++) {
                      if (b.votings[i].status === true ) {
                        countB++;
                      }
                    }

                    if (countA > countB) {
                      return -1;
                    } else if (countA < countB) {
                      return 1;
                    } else {
                      if (a.updatedAt > b.updatedAt) {
                        return -1;
                      } else if (a.updatedAt < b.updatedAt) {
                        return 1;
                      }
                    }
                    return 0;
                  });
                });
    this.userService.isAuthenticated.subscribe(data => {
      this.isAuth = data;
    });
  }

}
