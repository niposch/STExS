import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
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
export class IndentedDropListComponent implements OnInit {
  @Output()
  public dropEvent: EventEmitter<Array<any>> = new EventEmitter<Array<any>>();

  @Input()
  cdkDropListData: ParsonExerciseLineDetailItem[] = [];
  @Input()
  cdkDropListConnectedTo: (CdkDropList | string)[] | CdkDropList | string = [];
  private maxIndentation = 15;
  constructor() { }

  ngOnInit(): void {
  }

  drop<T>($event: CdkDragDrop<T[], any>) {
    console.log($event);
    if($event.previousContainer === $event.container) {
      let targetElement = $event.container.getSortedItems()[$event.currentIndex].element.nativeElement
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
      // only indenting
      else if(currElement === targetElement){
        // @ts-ignore
        currElement.style.marginLeft = `${currIndentations * 20}px`;
        // @ts-ignore
        currElement.indentation = currIndentations;
      }
      else{
        // @ts-ignore
        currElement.style.marginLeft = `${currIndentations * 20}px`;
        // @ts-ignore
        currElement.indentation = currIndentations;
      }

    }
    else{
      transferArrayItem($event.previousContainer.data,
        $event.container.data,
        $event.previousIndex,
        $event.currentIndex);
    }
  }

  entered<T>($event: CdkDragEnter<T>) {
    console.log($event);
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
}
