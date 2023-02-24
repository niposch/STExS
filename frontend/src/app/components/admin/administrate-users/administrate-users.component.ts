import {Component, OnInit, ViewChild} from '@angular/core';
import {UserManagementService} from "../../../../services/generated/services/user-management.service";
import {UserListModel} from "../../../../services/generated/models/user-list-model";
import {firstValueFrom, lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatPaginator} from "@angular/material/paginator";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {LiveAnnouncer} from "@angular/cdk/a11y";
import {RoleType} from "../../../../services/generated/models/role-type";
import {MatDialog} from "@angular/material/dialog";
import {ChangeRoleDialogComponent} from "./change-role-dialog/change-role-dialog.component";
import {UserService} from "../../../services/user.service";

@Component({
  selector: 'app-administrate-users',
  templateUrl: './administrate-users.component.html',
  styleUrls: ['./administrate-users.component.scss']
})
export class AdministrateUsersComponent implements OnInit {

  public users: UserListModel[] | undefined;
  public isLoading: boolean | undefined;
  displayedColumns: string[] = ['lastName', 'firstName', 'highestRoleType', 'email', 'matrikelNumber' ,'actions'];
  // @ts-ignore
  @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ts-ignore
  public dataSource : MatTableDataSource<UserListModel>;
  // @ts-ignore
  @ViewChild(MatSort) sort: MatSort;
  public roles = RoleType;
  public searchString = "";
  constructor(private userManagementService : UserManagementService,
              private snackBar: MatSnackBar,
              private _liveAnnouncer: LiveAnnouncer,
              public dialog: MatDialog,
              private userService: UserService) { }

  ngOnInit(): void {
    this.isLoading = true;
    void this.loadUsers().then(() => {
      this.dataSource = new MatTableDataSource<UserListModel>(this.users);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.isLoading = false;
    });
  }

  async loadUsers() {
    await lastValueFrom(this.userManagementService.apiUserManagementUserListsGet$Json())
      .then((users: UserListModel[]) => {
        this.users = users;
      })
      .catch((error) => {
        console.log(error);
        this.snackBar.open("Error while loading users", "Close")
      })
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  async openDialog(user: UserListModel): Promise<void> {
    let canChangeRole = await this.canChangeRoles(user);
    if (!canChangeRole) {
      this.snackBar.open("You can't change your own role", "Close", {duration: 2000})
      return;
    }

    const dialogRef = this.dialog.open(ChangeRoleDialogComponent, {
      data: {user: user},
    });

    await lastValueFrom(dialogRef.afterClosed()).then(result => {
      if (result != null) {
        user.highestRoleType = result;
        this.changeRole(user);
      }
    });
  }

  async changeRole(user : UserListModel) {
    await lastValueFrom(this.userManagementService.apiUserManagementChangeRolePost$Json({
      userId : user.userId,
      newHighestRole: user.highestRoleType
    }))
      .then(() => {
        this.snackBar.open("Role changed successfully!", "Close", {duration: 2000})
      })
      .catch((error) => {
        console.log(error);
        this.snackBar.open("Error while changing role", "Close")
      })
  }

  async canChangeRoles(user: UserListModel) {
    let currentUser = await firstValueFrom(this.userService.currentUser)
    return currentUser?.id != user.userId;
  }

  searchUsers() {
    this.dataSource.data = this.users!.filter((user) => {
        return user.firstName?.toLowerCase().includes(this.searchString.toLowerCase())
        || user.lastName?.toLowerCase().includes(this.searchString.toLowerCase())
        || user.email?.toLowerCase().includes(this.searchString.toLowerCase())
        || user.matrikelNumber?.toString().includes(this.searchString);
    })

  }
}
