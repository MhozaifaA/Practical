/*
MIT License

Copyright (c) 2023 Huzaifa Aseel

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';


 function isString(value: any): value is string {
  return typeof value === "string";
}
 function isNumber(value: any): value is number {
  return typeof value === "number" && Number(value) == value;
}
 function isObject(value: any): value is object {
  return typeof value === "object" && value !== null;
}
 function isArray(value: any): value is Array<unknown> {
  return Array.isArray(value);
}

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit {

  private _list: any[] = [];
  @Input('list')
  set List(value: any[]) {
    this._list = value;
    this.OnTextChange({});
  }
  get List(): any[] {
    return this._list;
  }

  @Input() entities: any[] = [];
  @Output() entitiesChange = new EventEmitter<any[]>();

  sort = "asc";//desc
  Text: any;//to save lastvalue
  constructor() { }

  ngOnInit(): void {

  }

  timer: any;
  loading = false;

  OnTextChange(event: any) {
    
    clearTimeout(this.timer);
    this.loading = true;
    this.timer = setTimeout(() => {
     
      const text = event?.target?.value?.trimStart()?.toUpperCase()?.trim();
      if (text == null || text == '') {


        this.entitiesChange.emit(this.List);
      } else {
        let data = this.List.filter(x =>this.filterDeep(x,text));
        this.entitiesChange.emit(data);
      }

      this.loading = false;
      clearTimeout(this.timer);
      this.Text = text;
    }, 300)

  }

  filterDeep(val: any, text: string): boolean {
    
    if (val === undefined || val === null) {
      return false;
    }
    if (isString(val)) {
      return val.toUpperCase().includes(text);
    }
    if (isNumber(val)) {
      return this.filterDeep(val.toString(), text);
    }
    if (isObject(val)) {
      let vals = Object.values(val);
      return vals.some(x => this.filterDeep(x, text));
    }
    if (isArray(val)) {
      return val.some(x => this.filterDeep(x, text));
    }

    return false;
  }


  handleSort() {
    if (this.sort == "asc")
      this.sort = "desc";
    else
      this.sort = "asc"
    let data = [...this.entities].reverse();
    this.entitiesChange.emit(data);
  }



}
