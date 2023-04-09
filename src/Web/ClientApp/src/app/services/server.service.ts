import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BACKEND_URL_BASE } from 'src/config';
import { Server } from '../models/server';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  getAll(): Observable<Server[]> {
    return this.http.get<Server[]>(`${BACKEND_URL_BASE}/api/server/getAll`);
  }

  connect(server: Server){
    const userId = this.jwtHelper.decodeToken(localStorage.getItem('authToken') ?? "").sub;
    return this.http.get(`${BACKEND_URL_BASE}/api/server/connect/${userId}/${server.id}`);
  }
}
