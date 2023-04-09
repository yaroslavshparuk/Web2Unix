import { Component, OnInit } from '@angular/core';
import { Observable, shareReplay } from 'rxjs';
import { Server } from 'src/app/models/server';
import { ServerService } from 'src/app/services/server.service';

@Component({
  selector: 'servers',
  templateUrl: './servers.component.html',
  styleUrls: ['./servers.component.css']
})
export class ServersComponent implements OnInit {
  public servers$: Observable<Server[]> = new Observable<Server[]>();
  constructor(private serverService: ServerService) { }

  ngOnInit(): void {
    this.servers$ = this.serverService.getAll().pipe(shareReplay());
  }

  connect(server: Server): void {
    console.log(server)
  }
}
