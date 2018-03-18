import { Component, OnInit } from '@angular/core';
import { ProductsService } from "../_services/product.service";
import { GroupItem } from "../_models/groupItem";
import { Product } from "../_models/product";

@Component({
  selector: 'product-statistic-page',
  templateUrl: './product-statistic-page.component.html',
  styleUrls: ['./product-statistic-page.component.css']
})
export class ProductStatisticPageComponent implements OnInit {
  length: number;
  pageSize = 3;
  productName: string;

  groups: GroupItem[] = [];
  public products: Array<Product> = [];

  currentPage: number = 0;
  groupsCount: number = 3;

  constructor(private productService: ProductsService ) { }

  ngOnInit() {
    this.getGrouped(this.currentPage);
    this.getProducts(this.currentPage);
  }

  getGrouped(page) {
    this.currentPage = page;
    this.productService.getGroups(this.currentPage, this.groupsCount)
      .subscribe(
      groupModel => {
        this.groups = groupModel.items;
        this.length = groupModel.totalCount;
      },
      error => {
      });
  }

  getProducts(page) {
    this.currentPage = page;
    this.productService.getProducts(this.currentPage, this.groupsCount, this.productName)
      .subscribe(
      pageModel => {
        this.products = pageModel.items;
      },
      error => {
      });
  }
}
