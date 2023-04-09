import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TerminalService } from 'src/app/services/terminal.service';
import { filter } from 'rxjs/operators';
import { Command } from 'src/app/models/command';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'terminal',
  templateUrl: './terminal.component.html',
  styleUrls: ['./terminal.component.css']
})
export class TerminalComponent implements OnInit {
  outputs: string[] = [];
  consoleInput = '';
  serverId: number | undefined;
  constructor(
    private terminalService: TerminalService,
    private route: ActivatedRoute,
    private userService: UserService) { }

  ngOnInit(): void {
    this.terminalService.connect().subscribe(output => {
      this.outputs.push(output);
    });
    this.serverId = Number(this.route.snapshot.queryParamMap.get('serverId'))
    console.log(this.serverId)
  }

  sendConsoleInput() {
    if (this.consoleInput.trim()) {
      this.outputs.push(`$ ${this.consoleInput}`);
      const command = new Command();
      command.userId = this.userService.getCurrentUserId();
      command.serverId = this.serverId;
      command.commandValue = this.consoleInput;
      this.terminalService.sendCommand(command);
      this.consoleInput = '';
    }
  }
}
