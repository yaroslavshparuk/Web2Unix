import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BACKEND_URL_BASE } from 'src/config';
import { Command } from '../models/command';
import { HttpClient } from '@angular/common/http';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class TerminalService {

  constructor(private http: HttpClient, private userService: UserService) { }

  connect(serverId: number) : Observable<string> {
    const userId = this.userService.getCurrentUserId();
    return this.http.post(`${BACKEND_URL_BASE}/api/terminal/connect`, {userId: userId, serverId: serverId }, {responseType: 'text'} );
  }

  disconnect(serverId: number) : Observable<string> {
    const userId = this.userService.getCurrentUserId();
    return this.http.post(`${BACKEND_URL_BASE}/api/terminal/disconnect`, {userId: userId, serverId: serverId }, {responseType: 'text'} );
  }

  sendCommand(command: Command) {
    return this.http.post(`${BACKEND_URL_BASE}/api/terminal/execute`, command, {responseType: 'text'} );
  }
}
