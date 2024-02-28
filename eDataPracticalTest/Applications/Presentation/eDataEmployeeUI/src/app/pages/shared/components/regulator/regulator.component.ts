/*
MIT License

Copyright (c) 2023 Huzaifa Aseel

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FilterComponent } from '../filter/filter.component';
import { PaginatorComponent } from '../paginator/paginator.component';

@Component({
  selector: 'app-regulator',
  standalone: true,
  imports: [CommonModule, FilterComponent, PaginatorComponent],
  templateUrl: './regulator.component.html',
  styleUrls: ['./regulator.component.css'],
})
export class RegulatorComponent implements OnInit {

  private _list: any[] = [];
  @Input('list') 
  set List(value: any[]) {
    this._list = value;
  }
  get List(): any[] {
    return this._list;
  }


  _filterList: any[] = [];

  @Input('title') title: string = "";

  @Input() value: any[] = [];
  @Output() valueChange = new EventEmitter<any[]>();


  @ViewChild("paginatorElement", { static: true }) paginatorElement!: ElementRef;

  @ViewChild("filterElement", { static: true }) filterElement!: ElementRef;

  

  constructor() { }

  ngOnInit(): void {
  }


  OnPagination(val: any[]) {
    this.valueChange.emit(val);
  }

}
