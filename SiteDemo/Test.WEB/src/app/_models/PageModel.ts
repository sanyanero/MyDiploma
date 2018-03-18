export class PageModel<T> {
  curentPage: number;
  items: T[];
  totalCount: number;

  constructor(curentPage?: number, items?: T[], totalCount?: number) {
    this.curentPage = curentPage;
    this.items = items;
    this.totalCount = totalCount;
  }
}
