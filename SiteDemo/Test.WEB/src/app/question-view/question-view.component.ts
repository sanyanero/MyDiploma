import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../_models/question';

@Component({
  selector: 'question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.css']
})
export class QuestionViewComponent implements OnInit {

  @Input() question: Question;

  constructor() { }

  ngOnInit() {
  }

}
