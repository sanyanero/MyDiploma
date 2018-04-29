import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';


import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PageModel } from "../_models/PageModel";
import { GroupItem } from "../_models/groupItem";
import { Question } from '../_models/question';



@Injectable()
export class QuestionsService {
  readonly BASEURL: string;
  constructor(private http: HttpClient) {
    this.BASEURL = environment.baseApi;
  }

  getQuestions(page: number, count: number, id: number): Observable<PageModel<Question>> {
    var url = 'api/questions/paged?page=' + page + '&count=' + count + '&id=' + id;
    return this.http.get<PageModel<Question>>(this.BASEURL + url);
  }

  getGroups(page: number, count: number, key?: string): Observable<PageModel<GroupItem>> {
    var url = 'api/questions/grouped?page=' + page + '&count=' + count + (key ? '&key=' + key : '');
    return this.http.get<PageModel<GroupItem>>(this.BASEURL + url);
  }

  addQuestion(model): Observable<Question> {
    return this.http.post<Question>(this.BASEURL + 'api/questions', model);
  }

  updateQuestion(model): Observable<Question> {
    return this.http.put<Question>(this.BASEURL + 'api/questions/' + model.id, model);
  }

  deleteQuestion(id): Observable<Question> {
    return this.http.delete<Question>(this.BASEURL + 'api/questions/' + id);
  }
}
