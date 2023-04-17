import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TerminalService } from 'src/app/services/terminal.service';
import { take } from 'rxjs/operators';
import { Command } from 'src/app/models/command';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'terminal',
  templateUrl: './terminal.component.html',
  styleUrls: ['./terminal.component.css']
})
export class TerminalComponent implements OnInit, OnDestroy {
  outputs: string[] = [];
  consoleInput = '';
  serverId: number = 0;
  constructor(
    private terminalService: TerminalService,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService) { }

  ngOnDestroy(): void {
    this.internalClose();
  }

  ngOnInit(): void {
    this.serverId = Number(this.route.snapshot.queryParamMap.get('serverId'))
    this.terminalService.connect(this.serverId).pipe(take(1)).subscribe(x => this.outputs.push(x));
  }

  sendConsoleInput() {
    if (this.consoleInput.trim()) {
      const command = new Command();
      command.userId = this.userService.getCurrentUserId();
      command.serverId = this.serverId;
      command.commandValue = this.consoleInput;
      this.terminalService.sendCommand(command).pipe(take(1)).subscribe(x => this.outputs.push(x));
      this.consoleInput = '';
    }
  }

  close(){
    this.internalClose();
    this.router.navigate(['/servers']);
  }

  private internalClose() {
    this.terminalService.disconnect(this.serverId).pipe(take(1)).subscribe();
  }
}
