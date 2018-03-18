import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductStatisticPage } from './product-statistic.component';

describe('ProductStatisticComponent', () => {
  let component: ProductStatisticPage;
  let fixture: ComponentFixture<ProductStatisticPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductStatisticPage ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductStatisticPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
