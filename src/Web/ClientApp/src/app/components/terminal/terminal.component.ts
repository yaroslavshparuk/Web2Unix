import { Component, OnInit } from '@angular/core';
import { TerminalService } from 'src/app/services/terminal.service';

@Component({
  selector: 'terminal',
  templateUrl: './terminal.component.html',
  styleUrls: ['./terminal.component.css']
})
export class TerminalComponent implements OnInit {
  outputs: string[] = [];
  consoleInput = '';

  constructor(private terminalService: TerminalService) { }

  ngOnInit(): void {
    this.terminalService.connect().subscribe(output => {
      this.outputs.push(output);
    });
  }

  sendConsoleInput() {
    if (this.consoleInput.trim()) {
      this.outputs.push(`$ ${this.consoleInput}`);
      this.terminalService.sendCommand(this.consoleInput);
      this.consoleInput = '';
    }
  }
}
