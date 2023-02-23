import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {UserManagementService} from "../../../../services/generated/services/user-management.service";
import {UserListModel} from "../../../../services/generated/models/user-list-model";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatPaginator} from "@angular/material/paginator";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {LiveAnnouncer} from "@angular/cdk/a11y";
import {RoleType} from "../../../../services/generated/models/role-type";
import {MatDialog} from "@angular/material/dialog";
import {ChangeRoleDialogComponent} from "./change-role-dialog/change-role-dialog.component";
import {UserService} from "../../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-administrate-users',
  templateUrl: './administrate-users.component.html',
  styleUrls: ['./administrate-users.component.scss']
})
export class AdministrateUsersComponent implements OnInit, AfterViewInit {

  public users: UserListModel[] | undefined;
  public isLoading: boolean = false;
  displayedColumns: string[] = ['lastName', 'firstName', 'highestRoleType', 'email', 'matrikelNumber' ,'actions'];
  // @ts-ignore
  @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ts-ignore
  public dataSource : MatTableDataSource<UserListModel>;
  // @ts-ignore
  @ViewChild(MatSort) sort: MatSort;
  public roles = RoleType;
  constructor(private userManagementService : UserManagementService,
              private snackBar: MatSnackBar,
              private _liveAnnouncer: LiveAnnouncer,
              public dialog: MatDialog,
              private userService: UserService,
              private router: Router) { }

  canActivate(): boolean {
    if (this.userService.currentUser != null) {
      if (this.userService.currentRolesSubject.value!.includes(RoleType.Admin)) {
        return true;
      }
    }
    this.router.navigate(['/dashboard']);
    return false;
  }

  ngOnInit(): void {
    this.canActivate();
  }

  ngAfterViewInit(): void {
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

  openDialog(user : UserListModel): void {
    const dialogRef = this.dialog.open(ChangeRoleDialogComponent, {
      data: {user: user},
    });

    dialogRef.afterClosed().subscribe(result => {
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
}
