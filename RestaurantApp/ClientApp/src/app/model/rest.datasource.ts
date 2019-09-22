import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user.model';

const PROTOCOL = 'http';
const PORT = 5000;

@Injectable()
export class RestDatasource {
  baseUrl: string;
  authToken: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${PROTOCOL}://${location.hostname}:${PORT}`;
  }

  getUsers(): Observable<User[]> {
    console.log('getUsers()');
    console.log(this.baseUrl + '/api/user');
    return this.http.get<User[]>(this.baseUrl + '/api/user');
  }

  authenticate(user: string, pass: string): Observable<boolean> {
    return this.http.post<any>(this.baseUrl + '/api/user/login', { name: user, password: pass }).pipe(map(responce => {
      this.authToken = responce.success ? responce.token : null;
      return responce.success;
    }));
  }
}
