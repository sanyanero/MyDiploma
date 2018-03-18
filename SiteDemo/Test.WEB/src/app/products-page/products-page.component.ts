import { Component, OnInit, Output } from '@angular/core';

import { BaseTosterService } from "../_services/base-toaster.service";
import { PageModel } from "../_models/PageModel";
import { PageEvent } from "@angular/material/paginator";
import { EventEmitter } from "events";
import { Question } from '../_models/question';
import { QuestionsService } from '../_services/questions.service';


@Component({
  selector: 'products-page',
  templateUrl: './products-page.component.html',
  styleUrls: ['./products-page.component.css']
})

export class ProductsPageComponent implements OnInit {

  length: number;
  pageSize = 4;

  question: Question = new Question();

  currentPage: number = 0;
  productCount: number = 4;
  productName: string;

  public questions: Array<Question> = [];
  public pageModel: any;

  constructor(private questionsService: QuestionsService,
    private toasterService: BaseTosterService) {
  }

  ngOnInit() {
    this.getQuestions(this.currentPage);
  }

  addProduct() {
    this.questionsService.addQuestion(this.question)
      .subscribe(
      product => {
        this.toasterService.success();
        this.getQuestions(this.currentPage);
      },
      error => {
        this.toasterService.error();
      });
  }

  updateProduct() {
    this.questionsService.updateQuestion(this.question)
      .subscribe(
      product => {
        this.toasterService.success();
      },
      error => {
        this.toasterService.error();
      });
  }

  deleteQuestion(question) {
    this.questionsService.deleteQuestion(question.id)
      .subscribe(
      product => {
        this.toasterService.success();
        this.getQuestions(this.questions.length == 1 && this.currentPage !== 0 ? this.currentPage - 1 : this.currentPage);
      },
      error => {
        this.toasterService.error();
      });
  }

  getQuestions(page) {
    this.currentPage = page;
    this.questionsService.getQuestions(this.currentPage, this.productCount, this.productName)
      .subscribe(
      pageModel => {
        this.questions = pageModel.items;
        this.length = pageModel.totalCount;
      },
      error => {
      });
  }

  selectQuestion(product) { //select a product to edit
    this.question = product;
  }
}
