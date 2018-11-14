import { environment } from '../../environments/environment';
import { JwtService } from './jwt.service';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { map, take } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: Http, private httpClient: HttpClient,
                private jwt: JwtService) { }

  getRequest(url: string) {
    const completeUrl: string = environment.api_url + `${url}`;
    return this.http.get(completeUrl);
  }

  postRequest(url: string, obj: object) {
    const completeUrl: string = environment.api_url + `${url}`;
    return this.httpClient.post(completeUrl, obj);
  }

  putRequest(url: string, obj: object) {
    return this.httpClient.put(environment.api_url + `${url}`, obj);
  }

  deleteRequest(url: string, obj: object, header: object = {}) {
    return this.http.delete(environment.api_url + `${url}`);
  }

  getRequestAuth(url: string) {
    const completeUrl: string = environment.api_url + `${url}`;
    const headers = new HttpHeaders({
      'Token': this.jwt.getToken().toString() });
    return this.httpClient.get(completeUrl, { headers: headers });
  }

  postRequestAuth(url: string, obj: object) {
    const completeUrl: string = environment.api_url + `${url}`;
    console.log(obj);
    if (this.jwt.getToken()) {
      return this.httpClient.post(completeUrl, obj, { headers: { token: this.jwt.getToken()}});
    }
    return this.httpClient.post(completeUrl, obj);
  }

  putRequestAuth(url: string, obj: object) {
    return this.httpClient.put(environment.api_url + `${url}`, obj, { headers: { token: this.jwt.getToken()}});
  }

  deleteRequestAuth(url: string, obj: object) {
    if (this.jwt.getToken()) {
      const completeUrl: string = environment.api_url + `${url}`;
      return this.httpClient.delete(completeUrl, { headers: { Authorization: 'Token ' +  this.jwt.getToken() }});
    }
    return this.http.delete(environment.api_url + `${url}`);
  }
}
