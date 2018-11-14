import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtService } from './jwt.service';
import { ApiService } from './api.service';
import { Injectable, OnInit } from '@angular/core';
import { Observable ,  BehaviorSubject ,  ReplaySubject } from 'rxjs';
import { map, distinctUntilChanged } from 'rxjs/operators';
import { User } from '../models/user.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnInit {
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());

  private isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();


  constructor(private api: ApiService, private jwt: JwtService, private http: HttpClient) { }
  articles: any;

  populate() {
    if (this.jwt.getToken()) {
      this.api.getRequestAuth('/users/current')
      .pipe(map(res => res))
      .subscribe(
        data => this.setAuth(data),
        err => this.purgeAuth()
      );
    } else {
      this.purgeAuth();
    }
  }

  authUser(email: string, password: string) {

    const obj = {
        EmailID: email,
        Password: password
    };
    return this.api.postRequest('/users/login', obj).pipe(map(res => res));
  }

  registerUser(form: any) {
    // this.api.postRequest('/users', form).subscribe(user => console.log(user));
    const headeres = new HttpHeaders({
      'ContentType': 'false',
      'mimeType': 'multipart/form-data' });
    return this.http.post(environment.api_url + '/users', form , { headers: headeres });
  }

  setAuth (user) {
    this.jwt.saveToken(user.token.tokenString);
    console.log(this.jwt.getToken());

    console.log(user);
    this.currentUserSubject.next(user);

    this.isAuthenticatedSubject.next(true);
  }

  getProfile(username) {
    return this.api.getRequest('/profiles/' + username).pipe(map(res => res.json()));
  }

  purgeAuth () {
    this.jwt.deleteToken();
    console.log(this.jwt.getToken());

    this.currentUserSubject.next({} as User);

    this.isAuthenticatedSubject.next(false);
  }

  ngOnInit() { }
}
