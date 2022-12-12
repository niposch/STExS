import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {Module} from "../../../../services/generated/models/module";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-modules-user',
  templateUrl: './modules-user.component.html',
  styleUrls: ['./modules-user.component.scss']
})
export class ModulesUserComponent implements OnInit {
  userName: string = "";
  isAdmin: boolean = false;
  moduleList: Array<ModuleListItem> | null = null;

  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) {
  }

  ngOnInit(): void {
    this.userService.currentUserSubject.subscribe(u => {
      if (u == null) return;
      firstValueFrom(this.moduleService.apiModuleGetModulesForUserGet$Json({
        userId: u.id ?? ""
      }))
        .then(modules => {
          this.moduleList = modules
            .map(m => {return {module:m, isOwner: u.id == m.ownerId} as ModuleListItem})
        })
    })
  }
}

interface ModuleListItem{
  module:Module,
  isOwner:boolean
}
