
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { MatTabsModule } from '@angular/material/tabs';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToasterModule, ToasterService } from "angular2-toaster";


import { BaseTosterService } from "./_services/base-toaster.service";

import { AppComponent } from "./app.component";


import {MatPaginatorModule} from '@angular/material/paginator';
import { TestsPageComponent } from "./tests-page/tests-page.component";
import { QuestionsService } from "./_services/questions.service";
import { NavbarComponent } from './navbar/navbar.component';
import { TestPageComponent } from './test-page/test-page.component';
import { QuestionViewComponent } from './question-view/question-view.component';

const appRoutes: Routes = [
  { path: "tests", component: TestsPageComponent },
  { path: "test/:id", component: TestPageComponent },

  {
    path: "",
    redirectTo: "/home",
    pathMatch: "full"
  }
];


@NgModule({
  declarations: [
    AppComponent,
    TestsPageComponent,
    NavbarComponent,
    TestPageComponent,
    QuestionViewComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    HttpModule,
    ToasterModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MatTabsModule,
    MatPaginatorModule
  ],
  providers: [
    BaseTosterService,
    QuestionsService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
