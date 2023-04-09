import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user = new User();
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login(user: User) {
    this.authService.login(user).subscribe(token => {
      localStorage.setItem('authToken', token);
      this.router.navigate(['/servers']);
    });
  }
}
