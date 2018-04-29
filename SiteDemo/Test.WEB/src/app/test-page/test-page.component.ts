import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionsService } from '../_services/questions.service';
import { Question } from '../_models/question';

@Component({
  selector: 'test-page',
  templateUrl: './test-page.component.html',
  styleUrls: ['./test-page.component.css']
})
export class TestPageComponent implements OnInit {

  id: number;
  private sub: any;

  length: number;
  currentPage: number = 0;
  questionsCount: number = 10;

  questions: Array<Question> = [];

  constructor(private route: ActivatedRoute,
    private questionService: QuestionsService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => { // takes id param from route link
      this.id = params['id']; // (+) converts string 'id' to a number
      this.getQuestions(this.currentPage);
    });
  }


  getQuestions(page) {
    this.currentPage = page;
    this.questionService.getQuestions(this.currentPage, this.questionsCount, this.id)
      .subscribe(
      pageModel => {
        this.questions = pageModel.items;
        this.length = pageModel.totalCount;
      });
  }

}
