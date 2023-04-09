import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BACKEND_URL_BASE } from 'src/config';
import { Server } from '../models/server';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Server[]> {
    return this.http.get<Server[]>(`${BACKEND_URL_BASE}/api/server/getAll`);
  }
}
