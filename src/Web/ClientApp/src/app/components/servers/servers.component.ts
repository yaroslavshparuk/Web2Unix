import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  constructor(private serverService: ServerService, private router: Router) { }

  ngOnInit(): void {
    this.servers$ = this.serverService.getAll().pipe(shareReplay());
  }

  connect(serverId: number | undefined): void {
    //this.serverService.connect(server).subscribe(x => console.log(x));
    console.log(serverId)
    this.router.navigate(['/terminal'], { queryParams: { serverId: serverId } });

  }
}
