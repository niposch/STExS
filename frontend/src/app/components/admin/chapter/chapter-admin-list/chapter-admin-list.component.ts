import {Component, Input, OnInit} from '@angular/core';
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";
import {ModuleParticipationStatus} from "../../../../../services/generated/models/module-participation-status";
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ChapterCreateItem} from "../../../../../services/generated/models/chapter-create-item";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-chapter-admin-list',
  templateUrl: './chapter-admin-list.component.html',
  styleUrls: ['./chapter-admin-list.component.scss']
})
export class ChapterAdminListComponent implements OnInit {

  @Input()
  moduleId: string = null!;

  module: ModuleDetailItem | undefined;
  participationStatus: ModuleParticipationStatus | undefined;

  moduleParticipationStaus = ModuleParticipationStatus
  chapters: Array<ChapterDetailItem> | undefined;

  chapterCreateItem: ChapterCreateItem = {
    chapterName: "",
    chapterDescription: ""
  } as ChapterCreateItem

  public showLoading: boolean = false;
  public isLoadingChapters: boolean = true;

  constructor(
    private readonly chapterService: ChapterService,
    private readonly moduleService: ModuleService,
    private snackBar : MatSnackBar,
  ) {
  }

  ngOnInit(): void {
    this.isLoadingChapters = true;
    this.loadData(this.moduleId)
    this.isLoadingChapters = false;
  }

  loadData(moduleId: string | null) {
    if (moduleId == null) {
      return;
    }
    this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
      moduleId: moduleId
    })
      .subscribe(data => this.participationStatus = data)

    this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })
      .subscribe(data => this.module = data)

    this.chapterService.apiChapterForModuleGet$Json({
      moduleId: moduleId
    })
      .subscribe(data => this.chapters = data)

  }

  createChapter() {
    if ((this.chapterCreateItem.chapterName?.length ?? 0) == 0) {

      return;
    }
    if ((this.chapterCreateItem.chapterDescription?.length ?? 0) == 0) {
      return;
    }

    this.showLoading = true;

    this.chapterCreateItem.moduleId = this.moduleId;
    this.chapterService.apiChapterPost$Json({
      body: this.chapterCreateItem
    }).subscribe(_ => {
        this.loadData(this.moduleId);
        this.chapterCreateItem.chapterName = ""
        this.chapterCreateItem.chapterDescription = ""
        this.showLoading = false;
        this.snackBar.open('Successfully created Chapter', 'Dismiss', {duration:2000});
      })
  }
}
