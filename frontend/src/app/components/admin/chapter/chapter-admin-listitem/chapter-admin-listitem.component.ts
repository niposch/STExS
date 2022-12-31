import {Component, Input, OnInit} from '@angular/core';
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

  constructor() { }

  ngOnInit(): void {
    this.chapterName = this.chapter?.chapterName;
    this.chapterDescription = this.chapter?.chapterDescription;
  }

  editButtonClick($event: MouseEvent) {
    $event.stopPropagation();
  }
}
