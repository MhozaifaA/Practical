import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pivot-column, [pivot-column]',
  templateUrl: './pivot-column.component.html',
  styleUrls: ['./pivot-column.component.css']
})
export class PivotColumnComponent implements OnInit {


  @Input() mini: boolean = false;


  private _list: any[] = [];
  @Input('list')
  set List(value: any[]) {
    this._list = value;
    this.buildOptimizeListHeadersSelector();
  }
  get List(): any[] {
    return this._list;
  }

  private _headersSelector: string[] = [];
  @Input('selector')
  set headersSelector(value: string[]) {
    this._headersSelector = value;
    this.buildOptimizeListHeadersSelector();
  }
  get headersSelector(): string[] {
    return this._headersSelector;
  }


  @Input() entities: any[] = [];
  @Output() entitiesChange = new EventEmitter<any[]>();

  constructor() { }

  ngOnInit(): void {
   



  }


  optomizeListHeadersSelector: Record<string, any[]> = {};

  buildOptimizeListHeadersSelector() {
    if (this.List.length == 0 || this.headersSelector.length == 0) 
      return;

    this.entitiesChange.emit(this.List);
    this.optomizeListHeadersSelector = {};//reset
    //fill data as column name
    for (var i = 0; i < this.List.length; i++) {

      let item = <any>this.List[i]; //git item
      //distribute attr to list
      for (var j = 0; j < this.headersSelector.length; j++) {

        const col = this.headersSelector[j];
        if (col == '') //enable gap empty
          continue;


        if (!this.optomizeListHeadersSelector[col]) {
          this.optomizeListHeadersSelector[col] = []; //init
        }
        //add value to attr
        this.optomizeListHeadersSelector[col].push(item[col])

      }

    }

    //sort
    for (var j = 0; j < this.headersSelector.length; j++) {
      const col = this.headersSelector[j];
      if (col == '') //enable gap empty
        continue;
      if (!this.filterByHead[col]) {
        this.filterByHead[col] = { load: false, text: null, selected: null };
      }
      //distinict and sort
      this.optomizeListHeadersSelector[col] =
        [...new Set(this.optomizeListHeadersSelector[col])].sort((a, b) => {
          return (a?.toLowerCase() ?? '') < (b?.toLowerCase() ?? '')
            ? -1
            : 1
        });
    }


    this.entitiesChange.emit(this.List);

  }

  filterbyHeaderSelector(col: string): string[] {
    if (this.filterByHead[col].load === false)
      return [];
    return this.optomizeListHeadersSelector[col];
  }

  OnHeaderLoad(col: string) {
    this.filterByHead[col].load = true;
  }

  filterByHead: Record<string, { load: boolean, text: string | null, selected: (any[] | null) }> = {};


  timer: any;
  fisrtFilterHeaderChanged = true;
  FilterByHeadChanged() {
    clearTimeout(this.timer);

    //console.log(this.optomizeListHeadersSelector);
    //console.log(this.headersSelector);
    //console.log(this.filterByHead);
    this.timer = setTimeout(() => {

      // '' = '_', null = 'All'
      //if (this.fisrtFilterHeaderChanged) {
      //  this.entities = { ...this.List }; //save memory
      //  this.fisrtFilterHeaderChanged = false;
      //}

      let data = this.List.filter((x: any) => {

        for (var j = 0; j < this.headersSelector.length; j++) {
          const col = this.headersSelector[j];
          if (col == '') //enable gap empty
            continue;

          const headfilter = this.filterByHead[col];

          const attr_value = x[col];//get attr value


          if (validSelectd(headfilter)) {

            if (headfilter.selected?.includes(attr_value == null ? '' : attr_value)) { //yes

              if (validText(headfilter.text)) {//check is valid text

                if ((attr_value + '').toUpperCase().includes(headfilter.text!.toUpperCase()) == false) {
                  return false; //one find attr not fit filter return false
                }

              }


              //continue
            } else { //no
              return false;
            }

          } else {

            if (validText(headfilter.text)) {
              //check is valid text

              if ((attr_value + '').toUpperCase().includes(headfilter.text!.toUpperCase()) == false) {
                return false; //one find attr not fit filter return false
              }
            }

            //continue

          }

        }

        return true;

      });


      this.entitiesChange.emit(data);

      clearTimeout(this.timer);

    }, 300);

    function validSelectd(reco: { load: boolean, text: string | null, selected: (any[] | null) }) {
      //check if not open enable laod that mean no need to check
      //get all no filter List inn this attr
      return !(reco.load == false || reco.selected == null || reco.selected.length == 0
        || reco.selected?.includes(null));
    }

    function validText(text: string | null) {
      return text != null && text != undefined && text != '';
    }

  }

}
