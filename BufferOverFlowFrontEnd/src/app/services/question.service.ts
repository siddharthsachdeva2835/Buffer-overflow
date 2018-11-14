import { map } from 'rxjs/operators';
import { ApiService } from './api.service';
import { Injectable, OnInit } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class QuestionService implements OnInit {

  constructor(private api: ApiService) { }

  getAllQuesions() {
    return this.api.getRequest('/questions').pipe(map(data => data.json()));
  }

  getAllQuesionsBySearchString(searchString: string) {
    console.log(searchString);

    return this.api.getRequest('/questions?searchstring=' + searchString).pipe(map(data => data.json()));
  }

  getQuestion(id) {
    return this.api.getRequest('/questions/' + id).pipe(map(data => data.json()));
  }

  addQuestion(obj: object) {
    return this.api.postRequestAuth('/questions', obj).pipe(map(data => data));
  }

  updateQuestion(questionID: number, obj: object) {
    return this.api.putRequest('/questions/' + questionID, obj).pipe(map(data => data));
  }

  deleteQuestion(questionID: number) {
    return this.api.deleteRequest('/questions/' + questionID, {});
  }

  ngOnInit() {

  }
}
