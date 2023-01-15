import {Component, Input, OnInit} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {} from "../../../../../services/generated/models/module-participation";

@Component({
  selector: 'app-member-list-admin',
  templateUrl: './member-list-admin.component.html',
  styleUrls: ['./member-list-admin.component.scss']
})
export class MemberListAdminComponent implements OnInit {

  //@Input() public participations: Array<ModuleParticipationDetailItem>

  constructor(private readonly moduleService:ModuleService) { }

  ngOnInit(): void {
  }


}
