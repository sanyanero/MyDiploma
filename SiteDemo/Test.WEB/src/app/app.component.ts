import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';

import { BaseTosterService } from './_services/base-toaster.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  pageTitle: string = '';
  errorMessage: string = '';
  baseToasterConfig:{};

  constructor(private tosterService: BaseTosterService) {
    this.baseToasterConfig = this.tosterService.baseToasterConfig;
  }

  ngOnInit(): void {
  }
}
