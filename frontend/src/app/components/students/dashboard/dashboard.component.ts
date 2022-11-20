import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public userName: string = "";

  constructor(private readonly userService: UserService) {
  }

  ngOnInit(): void {
    this.userService.currentUserSubject.subscribe(user => {
      if (user != null) {
        this.userName = user.userName;
      }
    })
  }

}
