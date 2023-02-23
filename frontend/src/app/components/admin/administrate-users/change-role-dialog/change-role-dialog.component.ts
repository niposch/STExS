import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UserListModel} from "../../../../../services/generated/models/user-list-model";
import {RoleType} from "../../../../../services/generated/models/role-type";

@Component({
  selector: 'app-change-role-dialog',
  templateUrl: './change-role-dialog.component.html',
  styleUrls: ['./change-role-dialog.component.scss']
})
export class ChangeRoleDialogComponent {

  public roles = RoleType;
  public newRole: RoleType = -1;
  constructor(public dialogRef: MatDialogRef<ChangeRoleDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: {user : UserListModel}) {}

  onNoClick(): void {
    this.dialogRef.close(null);
  }

}
