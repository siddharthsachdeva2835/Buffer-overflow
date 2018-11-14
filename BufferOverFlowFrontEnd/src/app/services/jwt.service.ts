import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  getToken(): string {
    return window.localStorage.getItem('token');
  }

  saveToken(token: string) {
    window.localStorage.setItem('token', token);
  }

  deleteToken() {
    window.localStorage.removeItem('token');
  }
}
