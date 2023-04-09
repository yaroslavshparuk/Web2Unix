import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AuthIntercoptor } from './interceptros/auth-interceptor';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { ServersComponent } from './components/servers/servers.component';
import { AuthService } from './services/auth.service';
import { ServerService } from './services/server.service';
import { TerminalService } from './services/terminal.service';
import { TerminalComponent } from './components/terminal/terminal.component';
import { UserService } from './services/user.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    ServersComponent,
    TerminalComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'servers', component: ServersComponent },
      { path: 'terminal', component: TerminalComponent },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('authToken')
      }
    }),
  ],
  providers: [
    JwtHelperService,
    ServerService,
    AuthService,
    TerminalService,
    UserService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthIntercoptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
