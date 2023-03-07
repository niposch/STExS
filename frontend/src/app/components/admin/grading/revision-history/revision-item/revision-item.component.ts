import {Component, Input, OnInit} from '@angular/core';
import {TimeTrackEvent} from "../../../../../../services/generated/models/time-track-event";
import {TimeTrackEventType} from "../../../../../../services/generated/models";

@Component({
  selector: 'app-revision-item',
  templateUrl: './revision-item.component.html',
  styleUrls: ['./revision-item.component.scss']
})
export class RevisionItemComponent implements OnInit {

  @Input()
  public event!: TimeTrackEvent;
  eventType= TimeTrackEventType;
  constructor() { }

  ngOnInit(): void {
  }

  getClass(type: TimeTrackEventType|undefined):string {
    if(type === undefined) return "";
    switch (type) {
      case TimeTrackEventType.FinalSubmission: return "final";
      case TimeTrackEventType.TemporarySubmission: return "temporary";
      case TimeTrackEventType.TimeTrackStart: return "start";
      case TimeTrackEventType.TimeTrackClosed: return "closed";
      case TimeTrackEventType.TimeTrackLostContact: return "lost-contact";
    }
  }
}
