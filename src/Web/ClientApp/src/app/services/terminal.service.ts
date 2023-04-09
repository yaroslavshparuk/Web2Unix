import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, from } from 'rxjs';
import { BACKEND_URL_BASE } from 'src/config';
import { Command } from '../models/command';

@Injectable({
  providedIn: 'root'
})
export class TerminalService {
  private connection: signalR.HubConnection;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${BACKEND_URL_BASE}/terminalHub`)
      .build();
  }

  public connect(): Observable<string> {
    return new Observable<string>(observer => {
      this.connection.on('output', (output: string) => {
        observer.next(output);
      });
      this.connection.start()
        .then(() => console.log('Connected to SignalR hub'))
        .catch((err) => console.error(err));
    });
  }

  public sendCommand(command: Command) {
    this.connection.send('sendInput', command)
      .then(() => console.log(`Sent command: ${command}`))
      .catch((err) => console.error(err));
  }

}