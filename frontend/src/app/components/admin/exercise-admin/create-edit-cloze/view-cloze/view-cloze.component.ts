import {
  Component,
  OnInit,
  OnChanges,
  Input,
  ViewChild,
  EventEmitter,
  Output,
  SimpleChanges,
  ChangeDetectorRef,
} from '@angular/core';

@Component({
  selector: 'app-view-cloze',
  templateUrl: './view-cloze.component.html',
  styleUrls: ['./view-cloze.component.scss'],
})
export class ViewClozeComponent implements OnInit, OnChanges {
  @Input() text: string | undefined | null = null;
  @Input() enableInputfield: boolean = true;

  @Input() showSolutionsFromText: boolean = false;

  @Input() answers: Array<string> | undefined | null = null;
  @Output() answersChange: EventEmitter<Array<string>> = new EventEmitter<
    Array<string>
  >();

  public textParts: Array<string> | undefined | null;

  constructor(private readonly changeDetectorRef: ChangeDetectorRef) {}

  ngOnInit(): void {
    if (this.text != null) {
      this.splitText(this.text);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['text'] && changes['text'].currentValue != null) {
      this.splitText(changes['text'].currentValue);
    }
    if (changes['answers'] && changes['answers'].currentValue != null) {
      this.answers = changes['answers'].currentValue;
      this.adjustLengthOfAnswers(
        changes['text']?.currentValue ?? this.text,
        changes['answers'].currentValue
      );
    }
    if (
      changes['enableInputfield'] &&
      changes['enableInputfield'].currentValue != null
    ) {
      this.enableInputfield = changes['enableInputfield'].currentValue;
    }
  }

  splitText(clozeText: string) {
    const regex = /\[\[[^\[]*\]\]/gm;
    this.textParts = clozeText.split(regex);

    this.adjustLengthOfAnswers(clozeText);

    if(this.showSolutionsFromText){
      this.answers = clozeText.match(regex)?.map((x) => x.replace(/\[|\]/g, '')) ?? [];
    }
    else{
      this.answersChange.emit(this.answers!);
    }
    this.changeDetectorRef.detectChanges();
  }

  adjustLengthOfAnswers(
    clozeText: string,
    answers: Array<string> | null = null
  ) {
    this.answers = answers ?? this.answers ?? [];
    const regex = /\[\[[^\[]*\]\]/gm;
    let clozeCount = clozeText.match(regex)?.length ?? 0;
    if (this.answers.length < clozeCount) {
      for (let i = this.answers.length; i < clozeCount; i++) {
        this.answers.push('');
      }
    }

    if (this.answers.length > clozeCount) {
      this.answers = this.answers.slice(0, clozeCount);
    }
    this.changeDetectorRef.detectChanges();
  }
}
