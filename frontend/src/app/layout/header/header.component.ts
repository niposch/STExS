import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {RoleType} from "../../../services/generated/models";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userName: string = "";
  loggedIn: boolean = false;
  isAdmin: boolean = false;
  isTeacher = false;

  @Input()  public hasDrawer: boolean = false;
  @Input()  public showDrawer: boolean = false;
  @Output()  public showDrawerChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  isLoading: boolean = false;


  constructor(private readonly userService: UserService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
    this.userService.currentUserSubject.subscribe(user => {
      if (user != null) {
        this.userName = user.userName ?? "";
        this.loggedIn = true;
      }
    });

    this.userService.currentRoles.subscribe(roles => {
      if(roles?.includes(RoleType.Admin)){
        this.isAdmin = true;
        this.isTeacher = true;
      }
      else if (roles?.includes(RoleType.Teacher)){
        this.isAdmin = false;
        this.isTeacher = true;
      }
      else{
        this.isAdmin = false;
        this.isTeacher = false;
      }
    })

  }

  logout() {
    this.isLoading = true;
    this.userService.logout().then( () =>{
      this.router.navigate(["/"]).then(() => {
        this.isLoading = false;
      });

    });
  }

  login() {
    this.isLoading = true;
    this.router.navigate(["/login"]).then(() => {
      this.isLoading = false;
    })
  }

  register() {
    this.isLoading = true;
    this.router.navigate(["/register"]).then(() => {
      this.isLoading = false;
    })
  }


  toggleDrawer() {
    this.showDrawer = !this.showDrawer;
    this.showDrawerChange.emit(this.showDrawer);
  }
}
