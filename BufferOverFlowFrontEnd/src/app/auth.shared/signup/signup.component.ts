import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';
import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  form: FormData;
  userImage = null;
  error: any;
  submitted = false;
  constructor(private userService: UserService, private router: Router,
        private fb: FormBuilder, private cd: ChangeDetectorRef) {
   }
  @ViewChild('fileInput') fileInput: ElementRef;

  signupForm = new FormGroup({
        UserName: new FormControl('', Validators.required),
        EmailID: new FormControl('', [Validators.required, Validators.email]),
        Password: new FormControl('', [Validators.required,
                                                    Validators.pattern('^(?=.*\\d)(?=.*[a-zA-Z])(?=.*[#$^+=!*()@%&]).{8,}$')]),
        FirstName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z][a-zA-Z\\s]+$')]),
        LastName: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z][a-zA-Z\\s]+$')]),
  });

  onSubmit() {
    this.submitted = true;
    console.log(this.signupForm.controls.Password.errors);
    if (this.signupForm.valid && this.userImage !== null) {
      this.form = new FormData();
      this.form.set('file', this.userImage);
      this.form.set('newUser', JSON.stringify(this.signupForm.value));
      console.log(this.form.get('file'));
      console.log(this.form.get('newUser'));

      this.userService.registerUser(this.form).subscribe(user => {
        console.log(user);
        this.router.navigate(['home']);
      });
    }
  }

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      const file = event.target.files[0];
      this.userImage = file;
    } else {
      this.userImage = null;
    }
  }

  ngOnInit() {
  }

}
