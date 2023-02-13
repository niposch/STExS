import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {ModuleDetailItem} from "../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../services/generated/services/module.service";
import {map} from "rxjs/operators";
import {RoleType} from "../../../services/generated/models/role-type";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  public firstName: string = "";
  public lastName: string = "";

  public isAdmin = false;
  public isLoading: boolean = false;
  participatingInModuleList: Array<ModuleDetailItem> | null = null;
  adminModuleList:Array<ModuleDetailItem> | null = null;
  allModules:Array<ModuleDetailItem> | null = null;
  constructor(private readonly userService: UserService,
              private readonly moduleService:ModuleService) {
  }

  loadModulesAdminOf(){
    this.isLoading = true;
    return this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).pipe(map(modules => {
      this.adminModuleList = modules
      this.isLoading = false;
    }))
  }

  loadModulesPartOf() {
    this.isLoading = true;
    return this.moduleService.apiModuleGetModulesForUserGet$Json({
    }).pipe(map(modules => {
      this.participatingInModuleList = modules
      this.isLoading = false;
    }));
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadModulesPartOf().subscribe(() => {
      this.userService.currentRolesSubject.subscribe(roles => {
        if (roles != null) {
          this.isAdmin = roles.includes(RoleType.Admin);
        }
        if (this.isAdmin) {
          this.loadModulesAdminOf().subscribe( () => {
            this.isLoading = false;
          });
        } else {
          this.isLoading = false;
        }
      })
    });
  }
}
