import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../../services/generated/services/module.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public firstName: string = "";
  public lastName: string = "";
  participatingInModuleList: Array<ModuleDetailItem> | null = null;

  constructor(private readonly userService: UserService,
              private readonly moduleService:ModuleService) {
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(u =>{
      this.firstName = u?.firstName ?? ""
      this.lastName = u?.lastName ?? ""
    })
    this.moduleService.apiModuleGetModulesForUserGet$Json()
      .subscribe(modules => this.participatingInModuleList = modules)

  }

}
