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

  public isLoading: boolean = false;
  participatingInModuleList: Array<ModuleDetailItem> | null = null;
  adminModuleList:Array<ModuleDetailItem> | null = null;
  allModules:Array<ModuleDetailItem> | null = null;

  constructor(private readonly userService: UserService,
              private readonly moduleService:ModuleService) {
  }

  loadModulesAdminOf(){
    this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).subscribe(modules => this.adminModuleList = modules)
  }

  loadModulesPartOf() {
    this.moduleService.apiModuleGetModulesForUserGet$Json({
    }).subscribe(modules => this.participatingInModuleList = modules)
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.userService.currentUser.subscribe(u =>{
      this.firstName = u?.firstName ?? ""
      this.lastName = u?.lastName ?? ""
    })

    this.loadModulesPartOf();
    this.loadModulesAdminOf();

    this.isLoading = false;
  }
}
