import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges, ViewChild
} from '@angular/core';
import {
  CdkDragDrop,
  CdkDragEnter,
  CdkDragExit, CdkDragMove, CdkDragStart,
  CdkDropList,
  moveItemInArray,
  transferArrayItem
} from "@angular/cdk/drag-drop";
import {ParsonExerciseLineDetailItem} from "../../../../services/generated/models/parson-exercise-line-detail-item";

@Component({
  selector: 'app-indented-drop-list',
  templateUrl: './indented-drop-list.component.html',
  styleUrls: ['./indented-drop-list.component.scss']
})
export class IndentedDropListComponent implements OnInit, OnChanges {

  @ViewChild("drop_list")
  public dropList: CdkDropList | undefined;

  @Input()
  cdkDropListData: ParsonExerciseLineDetailItem[] = [];
  @Output()
  cdkDropListDataChange: EventEmitter<ParsonExerciseLineDetailItem[]> = new EventEmitter<ParsonExerciseLineDetailItem[]>();
  @Input()
  cdkDropListConnectedTo: (CdkDropList | string | undefined)[] | CdkDropList | string = [];
  private maxIndentation = 15;
  constructor(
    private readonly changeDetectorRef: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
  }


  drop($event: CdkDragDrop<ParsonExerciseLineDetailItem[], any>) {
    console.log($event);
    let currParsonLine = $event.container.data[$event.previousIndex];
    if($event.previousContainer === $event.container) {
      let targetElement = $event.container.getSortedItems()[$event.currentIndex].element.nativeElement
      let targetParsonLine = $event.container.data[$event.currentIndex];
      let currElement = $event.item.element.nativeElement;
      let useWorkaround = targetElement !== currElement && $event.container.data[$event.currentIndex] === $event.container.data[$event.previousIndex];
      // @ts-ignore
      let currIndentations = currElement.currentindentation ?? 0;
      // @ts-ignore
      let targetElementIndentations = targetElement.indentation ?? 0;
      moveItemInArray($event.container.data, $event.previousIndex, $event.currentIndex);
      // workaround for cdkDragMove not actually moving elements with same text
      if(useWorkaround){

        // @ts-ignore
        targetElement.style.marginLeft = `${currIndentations * 20}px`;
        // @ts-ignore
        targetElement.currentindentation = currIndentations;

        // @ts-ignore
        currElement.style.marginLeft = `${targetElementIndentations * 20}px`;
        // @ts-ignore
        currElement.indentation = targetElementIndentations;
      }
      else{
        // @ts-ignore
        currElement.style.marginLeft = `${currIndentations * 20}px`;
        // @ts-ignore
        currElement.currentindentation = currIndentations;
      }
      currParsonLine.indentation = currIndentations;
      // @ts-ignore
      $event.item.element.nativeElement.indentation = currParsonLine.indentation;
    }
    else{
      console.log("transferring")
      transferArrayItem($event.previousContainer.data,
        $event.container.data,
        $event.previousIndex,
        $event.currentIndex);
      let currElement = $event.item.element.nativeElement;
      let currParsonLine = $event.container.data[$event.currentIndex];
      // @ts-ignore
      currParsonLine.indentation = currElement.currentindentation ?? 0;
      // @ts-ignore
      $event.item.element.nativeElement.style.marginLeft = `${currParsonLine.indentation * 20}px`;
      // @ts-ignore
      $event.item.element.nativeElement.indentation = currParsonLine.indentation;
    }

    this.cdkDropListDataChange.emit($event.container.data);
  }

  entered<T>($event: CdkDragEnter<T>) {
    // @ts-ignore
    $event.item.element.nativeElement.currentindentation  = $event.item.dropContainer.data[$event.currentIndex].indentation;
  }

  exited<T>($event: CdkDragExit<T>) {
    console.log($event);
  }

  dragMoved($event: CdkDragMove<ParsonExerciseLineDetailItem>) {
    let indentations = Math.floor($event.distance.x / 20);
    // @ts-ignore
    let currIndentations = $event.source.element.nativeElement.indentation ?? 0;
    indentations += currIndentations;
    if(indentations < 0) {
      indentations = 0;
    }
    if(indentations > this.maxIndentation){
      indentations = this.maxIndentation;
    }
    $event.source.element.nativeElement.style.marginLeft = `${indentations * 20}px`;
    // @ts-ignore
    $event.source.element.nativeElement.currentindentation = indentations;
    // @ts-ignore
    let placeholder = $event.event.target.querySelector('.cdk-drag-placeholder');
    if(placeholder != null && placeholder instanceof HTMLElement){
      placeholder.style.marginLeft = `${indentations * 20}px`;
    }
  }

  dragStarted($event: CdkDragStart) {
    // @ts-ignore
    let indentations = $event.source.dropContainer.data.indentation ?? 0;
    let placeholder = $event.source.dropContainer.element.nativeElement.querySelector('.cdk-drag-placeholder');
    // @ts-ignore
    $event.source.element.nativeElement.currentindentation = indentations;
    if(placeholder != null && placeholder instanceof HTMLElement){
      placeholder.style.marginLeft = `${indentations * 20}px`;
    }
    $event.source.dropped.subscribe((value: CdkDragDrop<ParsonExerciseLineDetailItem[], any>) => {
      console.log("dropped");
      this.cdkDropListDataChange.emit(this.cdkDropListData);
    });
  }

  ngOnChanges(changes: any): void {
    if(changes.cdkDropListData != null){
      this.cdkDropListData = changes.cdkDropListData.currentValue.filter((value: any) => value != null);
      this.changeDetectorRef.detectChanges();
      let elements = document.querySelectorAll('.cdk-drag');
      for(let i = 0; i < this.cdkDropListData.length; i++){
        let element = elements[i];
        if(element instanceof HTMLElement){
          // @ts-ignore
          element.indentation = this.cdkDropListData[i].indentation != null ?  this.cdkDropListData[i].indentation : 0;
        }
      }
    }
    if(changes.cdkDropListConnectedTo != null){
      this.cdkDropListConnectedTo = changes.cdkDropListConnectedTo.currentValue;
    }
  }
}
