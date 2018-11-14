import { map } from 'rxjs/operators';
import { Answer } from '../models/answer.model';
import { ApiService } from './api.service';
import { Injectable, OnInit } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AnswerService implements OnInit {

  constructor(private api: ApiService) { }

  addAnswer(questionID: number, obj: object) {
    console.log('adding answer');

    return this.api.postRequestAuth('/questions/' + questionID + '/answers', obj).pipe(map(data => data));
  }

  deleteAnswer(questionID: number, answerID: number) {
    return this.api.deleteRequest('/questions/' + questionID + '/answers/' + answerID, { });
  }

  updateAnswer (questionID, answerID: number, body: string) {
    return this.api.postRequest('/questions/' + questionID + '/answers/' + answerID, {body: body})
          .pipe(map(ans => ans));
  }

  upVote(questionID: number, answerID: number) {
    return this.api.postRequestAuth('/questions/' + questionID + '/answers/' + answerID + '/vote/1', { } );
  }

  downVote(questionID: number, answerID: number) {
    return this.api.postRequestAuth('/questions/' + questionID + '/answers/' + answerID + '/vote/0', { } );
  }

  ngOnInit() { }
}
