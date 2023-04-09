import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BACKEND_URL_BASE } from 'src/config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  public login(user: User): Observable<string>{
    return this.http.post(`${BACKEND_URL_BASE}/api/user/login`, user, {responseType: 'text'} );
  }

  public isLoggedIn() : boolean {
    const token = localStorage.getItem('authToken');
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  public logout() : void{
    localStorage.removeItem('authToken');
  }
}
