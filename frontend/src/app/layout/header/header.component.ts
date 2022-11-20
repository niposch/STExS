import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userName: string = "";
  loggedIn: boolean = false;

  constructor(private readonly userService: UserService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(user => {
      if (user != null) {
        this.userName = user.userName;
        this.loggedIn = true;
      } else {
        this.userName = "";
        this.loggedIn = false;
      }
    })
  }

  logout() {
    this.userService.logout();
    this.router.navigate(["/"])
  }
}
