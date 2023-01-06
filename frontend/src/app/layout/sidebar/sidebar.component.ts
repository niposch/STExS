import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {ModuleDetailItem} from "../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../services/generated/services/module.service";
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public firstName: string = "";
  public lastName: string = "";
  participatingInModuleList: Array<ModuleDetailItem> | null = null;
  adminModuleList:Array<ModuleDetailItem> | null = null;
  isModuleAdmin = false;

  constructor(private readonly userService: UserService,
              private readonly moduleService:ModuleService) {
  }

  async loadModules(){
    await this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).toPromise()
      .then(modules =>{
        this.adminModuleList = modules ?? []
      })
  }

  ngOnInit(): void {
    this.userService.currentUser.subscribe(u =>{
      this.firstName = u?.firstName ?? ""
      this.lastName = u?.lastName ?? ""
    })
    this.moduleService.apiModuleGetModulesForUserGet$Json()
      .subscribe(modules => this.participatingInModuleList = modules)

    this.loadModules();

  }

}
