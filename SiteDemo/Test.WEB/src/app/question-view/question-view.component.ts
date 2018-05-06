import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../_models/question';
import { Output } from '@angular/core/src/metadata/directives';
import { EventEmitter } from 'events';
import { debug } from 'util';

@Component({
  selector: 'question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.css']
})
export class QuestionViewComponent implements OnInit {

  @Input() question: Question;

  @Input() answerNum: number;

  correct: boolean;

  disabled: boolean;

  constructor() { }

  ngOnInit() {
  }


  saveAnswer(val) {
    if (val.value == this.answerNum) {
      this.correct = true;
      this.disabled = true;
    }
    else {
      this.correct = false;
      this.disabled = true;
    }
  }
}
