import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Product } from "../_models/product";

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { PageModel } from "../_models/PageModel";
import { GroupItem } from "../_models/groupItem";


//const BASEURL = "http://localhost:27121/";

@Injectable()
export class ProductsService {
    readonly BASEURL: string;
    constructor(private http: HttpClient) {
        this.BASEURL = environment.baseApi;
    }

    getProducts(page: number, count: number, name?: string, type?: string): Observable<PageModel<Product>> {
      var url = 'api/products?page=' + page + '&count=' + count + (type ? '&type=' + type : '') + (name ? '&name=' + name : '');
    return this.http.get<PageModel<Product>>(this.BASEURL + url);
    }

    getGroups(page: number, count: number, key?: string): Observable<PageModel<GroupItem>> {
      var url = 'api/products/grouped?page=' + page + '&count=' + count + (key ? '&key=' + key : '');
      return this.http.get<PageModel<GroupItem>>(this.BASEURL + url);
    }

    addProduct(model): Observable<Product> {
    return this.http.post<Product>(this.BASEURL + 'api/products', model);
    }

  updateProduct(model): Observable<Product> {
    return this.http.put<Product>(this.BASEURL + 'api/products/' + model.id, model);
  }

  deleteProduct(id): Observable<Product> {
    return this.http.delete<Product>(this.BASEURL + 'api/products/' + id);
  }
}
