import { QuestionService } from './../services/question.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '../../../node_modules/@angular/router';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css']
})
export class AddQuestionComponent implements OnInit {

  constructor(private questionService: QuestionService, private router: Router) { }
  tags = [];
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
    this.questionService.addQuestion(obj).subscribe(data => {
      this.router.navigate(['home']);
    });
  }

  ngOnInit() {
  }

}
