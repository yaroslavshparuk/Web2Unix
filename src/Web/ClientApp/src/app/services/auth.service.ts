import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  public login(user: User): Observable<string>{
    console.log(user)
    return this.http.post('https://localhost:7123/api/user/login', user, {responseType: 'text'} );
  }

  // public test(): Observable<any>{
  //   return this.http.post( this.baseUrl + 'api/user/test', user, {responseType: 'text'} );
  // }
}
