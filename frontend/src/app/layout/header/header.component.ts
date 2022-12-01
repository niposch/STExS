import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
  isAdmin: boolean = false;

  @Input()
  public hasDrawer: boolean = false;

  @Input()
  public showDrawer: boolean = false;
  @Output()
  public showDrawerChange: EventEmitter<boolean> = new EventEmitter<boolean>();


  constructor(private readonly userService: UserService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(user => {
      if (user != null) {
	    console.log(user);
        this.userName = user.userName;
        this.loggedIn = true;
		this.isAdmin = user.isAdmin;
		console.log(this);
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

  toggleDrawer() {
    this.showDrawer = !this.showDrawer;
    this.showDrawerChange.emit(this.showDrawer);
  }
}