/*
MIT License

Copyright (c) 2023 Huzaifa Aseel

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, NgModule, OnInit, Output } from '@angular/core';

export interface LinkModel {
  text?: string | null;
  page: number;
  enabled?: boolean;
  active?: boolean;
}


@Component({
  selector: 'app-paginator',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './paginator.component.html',
  styleUrl: './paginator.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PaginatorComponent implements OnInit {

  @Input('orientation') orientation: boolean = false;//vertical, false horz

  private _list: any[] = [];
  @Input('list')
  set List(value: any[]) {
    this._list = value;
    this.CurrentPage = 1;
    this.Total = this.List.length;
    this.LoadPages();
  }
  get List(): any[] {
    return this._list;
  }

  @Input('color') Color: string = "btn-primary";
  @Output('selectedpage') SelectedPage = new EventEmitter<{ page: number, quantity: number }>();

  @Input() entities: any[] = [];
  @Output() entitiesChange = new EventEmitter<any[]>();


  CurrentPage: number = 1;
  Total: number = 0;
  Radius: number = 3;
  Quantity: number = 12;
  links!: LinkModel[];


  constructor() { }



  ngOnInit(): void {

  }



  get TotaPagesQuantity() {
    // console.log(this.Total, this.Quantity)
    return Math.ceil(this.Total / this.Quantity)
  };

  SelectedQuantity(event: any) {
    this.CurrentPage = 1;
    this.Quantity = Number(event.target.value);
    this.LoadPages();
    this.SelectedPage.emit({ page: this.CurrentPage, quantity: this.Quantity });
  }

  SelectedPageInternal(link: LinkModel) {
    if (link.page == this.CurrentPage)
      return;

    if (!link.enabled)
      return;

    if (this.TotaPagesQuantity == 0)
      return;

    this.CurrentPage = link.page;
    this.SelectedPage.emit({ page: this.CurrentPage, quantity: this.Quantity });
    this.LoadPages();
  }

  private LoadPages() {
    this.links = [];
    let isPreviousPageLinkEnabled = this.CurrentPage != 1;
    let previousPage = this.CurrentPage - 1;

    this.links.push({ page: 1, enabled: isPreviousPageLinkEnabled, text: "«" });

    this.links.push({ page: previousPage, enabled: isPreviousPageLinkEnabled, text: "‹" });

    for (let i: number = 1; i <= this.TotaPagesQuantity; i++) {
      if (i >= this.CurrentPage - this.Radius && i <= this.CurrentPage + this.Radius) {
        this.links.push({ page: i, enabled: true, active: this.CurrentPage == i, text: i.toString() });
      }
    }

    var isNextPageLinkEnabled = this.CurrentPage != this.TotaPagesQuantity;
    var nextPage = this.CurrentPage + 1;

    this.links.push({ page: nextPage, enabled: isNextPageLinkEnabled, text: "›" });

    this.links.push({ page: this.TotaPagesQuantity, enabled: isNextPageLinkEnabled, text: "»" });

    let data = this.List.slice((this.CurrentPage - 1) * this.Quantity, this.CurrentPage * this.Quantity);

    this.entitiesChange.emit(data);
  }


}

