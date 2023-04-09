import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private jwtHelper: JwtHelperService) { }

  getCurrentUserId() : number{
    return Number(this.jwtHelper.decodeToken(localStorage.getItem('authToken') ?? "").sub);
  }
}
