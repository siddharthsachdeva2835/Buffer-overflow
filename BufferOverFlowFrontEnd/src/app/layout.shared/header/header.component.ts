
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { ApiService } from '../../services/api.service';
import { JwtService } from '../../services/jwt.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isAuth: boolean;
  user = new User();
  constructor(private userService: UserService, private router: Router, private jwt: JwtService) {
    this.isAuth = false;
  }

  logout() {
    this.userService.purgeAuth();
    this.router.navigate(['home/']);
  }

  ngOnInit() {
    this.userService.isAuthenticated.subscribe(data => this.isAuth = data);
    this.userService.currentUser.subscribe(data => this.user = data);
  }

}


