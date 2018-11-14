import { JwtService } from '../../services/jwt.service';
import { UserService } from '../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private userService: UserService, private jwt: JwtService , private router: Router) { }
  submitted = false;
  error: any;
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  onSubmit() {
    this.submitted = true;
    console.log(this.loginForm.controls.email.errors);

    if (this.loginForm.valid) {
      this.userService.authUser(this.loginForm.value.email, this.loginForm.value.password)
                            .subscribe(obj => {
                              this.userService.setAuth(obj);
                              this.router.navigate(['home']);
                            }, err => {
                              this.error = err;
                              console.log(this.error.error);
                            });
    }
  }

  ngOnInit() {
  }

}
