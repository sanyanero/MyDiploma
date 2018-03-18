import { Component, OnInit, Output } from '@angular/core';

import { Product } from "../_models/product";
import { ProductsService } from "../_services/product.service";
import { BaseTosterService } from "../_services/base-toaster.service";
import { PageModel } from "../_models/PageModel";
import { PageEvent } from "@angular/material/paginator";
import { EventEmitter } from "events";


@Component({
  selector: 'products-page',
  templateUrl: './products-page.component.html',
  styleUrls: ['./products-page.component.css']
})

export class ProductsPageComponent implements OnInit {

  length: number;
  pageSize = 4;

  product: Product = new Product();

  currentPage: number = 0;
  productCount: number = 4;
  productName: string;

  public products: Array<Product> = [];
  public pageModel: any;

  constructor(private productService: ProductsService,
    private toasterService: BaseTosterService) {
  }

  ngOnInit() {
    this.getProducts(this.currentPage);
  }

  addProduct() {
    this.productService.addProduct(this.product)
      .subscribe(
      product => {
        this.toasterService.success();
        this.getProducts(this.currentPage);
      },
      error => {
        this.toasterService.error();
      });
  }

  updateProduct() {
    this.productService.updateProduct(this.product)
      .subscribe(
      product => {
        this.toasterService.success();
      },
      error => {
        this.toasterService.error();
      });
  }

  deleteProduct(product) {
    this.productService.deleteProduct(product.id)
      .subscribe(
      product => {
        this.toasterService.success();
        this.getProducts(this.products.length == 1 && this.currentPage !== 0 ? this.currentPage - 1 : this.currentPage);
      },
      error => {
        this.toasterService.error();
      });
  }

  getProducts(page) {
    this.currentPage = page;
    this.productService.getProducts(this.currentPage, this.productCount, this.productName)
      .subscribe(
      pageModel => {
        this.products = pageModel.items;
        this.length = pageModel.totalCount;
      },
      error => {
      });
  }

  selectProduct(product) { //select a product to edit
    this.product = product;
  }
}
