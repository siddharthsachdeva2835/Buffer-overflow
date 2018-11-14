import { Question } from './../models/question.model';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionService } from './../services/question.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-update-question',
  templateUrl: './update-question.component.html',
  styleUrls: ['./update-question.component.css']
})
export class UpdateQuestionComponent implements OnInit {

  constructor(private questionService: QuestionService, private activatedRoute:
                    ActivatedRoute, private router: Router) { }
  tags = [];
  question: Question;
  questionForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    tag: new FormControl('')
  });

  addTag(e) {
    if (e.keyCode === 32) {
      console.log(this.questionForm.value.tag);

      this.tags.push(this.questionForm.value.tag);
      this.questionForm.patchValue({tag: ''});
    }
  }

  removeTag(tag) {
    this.tags.splice(this.tags.indexOf(tag), 1);
  }

  onSubmit() {
    const obj = {
        Title: this.questionForm.value.title,
        Description: this.questionForm.value.description,
        Tags: this.tags
    };
    this.questionService.updateQuestion(this.question.questionID, obj).subscribe();
    this.router.navigate(['home']);
  }

  ngOnInit() {
    this.questionService.getQuestion(this.activatedRoute.snapshot.paramMap.get('id'))
                .subscribe(data => {
                  this.question = data;
                  this.questionForm.setValue({title: this.question.title, description: this.question.description,
                                                tag: ''});
                  this.tags = data.tags;

                  // for (const tag of this..tagList) {
                  //   this.tagList.push(tag);
                  //   console.log(tag);
                  // }
                });
  }

}
