import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../module/delete-dialog/delete-dialog.component";
import {lastValueFrom} from "rxjs";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-chapter-admin-listitem',
  templateUrl: './chapter-admin-listitem.component.html',
  styleUrls: ['./chapter-admin-listitem.component.scss']
})
export class ChapterAdminListitemComponent implements OnInit {
  @Input() public chapter: ChapterDetailItem | null = null;
  public chapterName: string | null | undefined = "chapter_name";
  public chapterDescription: string | null | undefined = "chapter_description";

  @Input() public showEditButton: boolean = false;
  @Input() public showDeleteButton: boolean = false;
  @Input() public canReorder: boolean = false;
  private isDeleting: boolean = false;
  private showLoading: boolean = false;

  @Output() onChapterChange = new EventEmitter<any>();


  constructor(private readonly activatedRoute:ActivatedRoute,
    private readonly chapterService: ChapterService,
    private dialog: MatDialog,
    private readonly snackBar : MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {
    this.chapterName = this.chapter?.chapterName;
    this.chapterDescription = this.chapter?.chapterDescription;
  }


  editButtonClick($event: MouseEvent) {
    $event.stopPropagation();
    this.router.navigate(
      ['/module/administrate/chapter'],
      {queryParams: {chapterId:this.chapter?.id}}
    )
  }

  openDeleteDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "delete") this.deleteModule();
      }
    );
  }

  deleteModule() {
    if (this.isDeleting) {
      return;
    }
    this.showLoading = true;
    this.isDeleting = true;

    lastValueFrom(this.chapterService.apiChapterDelete({
      chapterId: this.chapter?.id
    }))
      .catch(err =>{
        this.snackBar.open("Could not delete Module", "Dismiss", {duration:5000})
        this.showLoading = false;
        this.isDeleting = false;
        throw err
      }).then(() => {
      this.isDeleting = false;
      this.showLoading = false;
      this.snackBar.open("Successfully deleted Module!", "Dismiss", {duration:5000})
      this.onChapterChange.emit();
    })
  }
}
