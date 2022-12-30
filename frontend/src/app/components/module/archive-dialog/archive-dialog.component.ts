import { Component, OnInit } from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-archive-dialog',
  templateUrl: './archive-dialog.component.html',
  styleUrls: ['./archive-dialog.component.scss']
})
export class ArchiveDialogComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<ArchiveDialogComponent>) { }

  ngOnInit(): void {
  }
    abort() {
    this.dialogRef.close("abort");
  }
  confirm() {
    this.dialogRef.close("archive");
  }
}
