import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
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
  @Output()
  public dropEvent: EventEmitter<Array<any>> = new EventEmitter<Array<any>>();

  @Input()
  cdkDropListData: ParsonExerciseLineDetailItem[] = [];
  @Input()
  cdkDropListConnectedTo: (CdkDropList | string)[] | CdkDropList | string = [];
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
        targetElement.indentation = currIndentations;

        // @ts-ignore
        currElement.style.marginLeft = `${targetElementIndentations * 20}px`;
        // @ts-ignore
        currElement.indentation = targetElementIndentations;
      }
      else{
        // @ts-ignore
        currElement.style.marginLeft = `${currIndentations * 20}px`;
        // @ts-ignore
        currElement.indentation = currIndentations;
      }
      currParsonLine.indentation = currIndentations;

    }
    else{
      transferArrayItem($event.previousContainer.data,
        $event.container.data,
        $event.previousIndex,
        $event.currentIndex);
    }
  }

  entered<T>($event: CdkDragEnter<T>) {
    // @ts-ignore
    $event.item.element.nativeElement.indentation = $event.container.data[$event.currentIndex].indentation
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
    let placeholder = $event.source.dropContainer.element.nativeElement.querySelector('.cdk-drag-placeholder');
    if(placeholder != null && placeholder instanceof HTMLElement){
      placeholder.style.marginLeft = `${indentations * 20}px`;
    }
  }

  dragStarted($event: CdkDragStart) {
    // @ts-ignore
    let indentations = $event.source.element.nativeElement.indentation ?? 0;
    let placeholder = $event.source.dropContainer.element.nativeElement.querySelector('.cdk-drag-placeholder');
    if(placeholder != null && placeholder instanceof HTMLElement){
      placeholder.style.marginLeft = `${indentations * 20}px`;
    }
  }

  ngOnChanges(changes: any): void {
    if(changes.cdkDropListData != null){
      this.cdkDropListData = changes.cdkDropListData.currentValue;
      this.changeDetectorRef.detectChanges();
      let elements = document.querySelectorAll('.cdk-drag');
      for(let i = 0; i < elements.length; i++){
        let element = elements[i];
        if(element instanceof HTMLElement){
          // @ts-ignore
          element.indentation = this.cdkDropListData[i].indentation;
        }
      }
    }
    if(changes.cdkDropListConnectedTo != null){
      this.cdkDropListConnectedTo = changes.cdkDropListConnectedTo.currentValue;
    }
  }
}
