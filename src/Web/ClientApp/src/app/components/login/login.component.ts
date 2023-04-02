 import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user = new User();
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  login(user: User){
    this.authService.login(user).subscribe(token =>{
      localStorage.setItem('authToken', token);
    });
  }

  // test(user: User){
  //   this.authService.login(user).subscribe(result =>{
  //     console.log(result)
  //   });
  // }

}
