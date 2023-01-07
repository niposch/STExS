import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";

@Component({
  selector: 'app-chapter-admin-listitem',
  templateUrl: './chapter-admin-listitem.component.html',
  styleUrls: ['./chapter-admin-listitem.component.scss']
})
export class ChapterAdminListitemComponent implements OnInit {

  @Input() chapter: ChapterDetailItem | null = null;

  public chapterName: string | null | undefined = "chapter_name";
  public chapterDescription: string | null | undefined = "chapter_description";
  public moduleId: string | null | undefined = "chapter_moduleid";

  constructor(private readonly activatedRoute:ActivatedRoute,
    private readonly moduleService: ModuleService,
    private router: Router) { }

  ngOnInit(): void {
    
    this.chapterName = this.chapter?.chapterName;
    this.chapterDescription = this.chapter?.chapterDescription;
    this.moduleId =  this.chapter?.moduleId;
  }

  editButtonClick($event: MouseEvent) {
    $event.stopPropagation();
  }
}
