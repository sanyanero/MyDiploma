
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

import { ProductsService } from "./_services/product.service";

import {MatPaginatorModule} from '@angular/material/paginator';
import { ProductsPageComponent } from "./products-page/products-page.component";
import { ProductStatisticPageComponent } from "./product-statistic-page/product-statistic-page.component";

const appRoutes: Routes = [
  { path: "products", component: ProductsPageComponent },
  { path: "statistic", component: ProductStatisticPageComponent },
  
  {
    path: "",
    redirectTo: "/products",
    pathMatch: "full"
  }
];


@NgModule({
  declarations: [
    AppComponent,
    ProductsPageComponent,
    ProductStatisticPageComponent
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
    ProductsService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
