import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpAppService } from '../../core/services/http-app.service';
import { DepartmentDto } from '../../core/models/DepartmentDto';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { PaginatorComponent } from '../shared/components/paginator/paginator.component';
import { RegulatorComponent } from '../shared/components/regulator/regulator.component';
import { FilterComponent } from '../shared/components/filter/filter.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [CommonModule, FormsModule, PaginatorComponent, RegulatorComponent, FilterComponent],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent implements OnInit {
  isLoaded = true;
  List: DepartmentDto[] = [];
  _filterList: DepartmentDto[] = [];
  _valueList: DepartmentDto[] = [];


  _model: DepartmentDto = {};
  isAdd = true;


  constructor(private httpApp: HttpAppService, private toastr: ToastrService,
    private changeDetectorRef: ChangeDetectorRef) {

  }

  ngOnInit(): void {

    this.httpApp.DepartmentList().Result((data) => {
      if (data)
        this.List = data!;
    });
  }
  
  value(e: DepartmentDto[]) {
    this._valueList = e;
    this.changeDetectorRef.detectChanges();
  }



  OpenAction(id?: string | null) {

    this.isAdd = id ? false : true;

    if (this.isAdd) {
      this._model = {};
    } else { //edit / delete
      this._model = { ...this._valueList.find(x => x.id == id)! };
    }
  }



  Action(form: NgForm) {
    if (form.invalid) {
      return;
    }
    if (this.isAdd) {

      this.httpApp.DepartmentAdd(this._model).Result(data => {
        this.toastr.success('susccesfully added');

        this.List = [data!, ...this.List];//not always, when desc will be last 
        this._model = {};

      })
    } else {
      this.httpApp.DepartmentEdit(this._model).Result(data => {
        this.toastr.success('susccesfully edited');
        let indexva = this._valueList.findIndex(x => x.id == data!.id)!;
        let indexlis = this.List.findIndex(x => x.id == data!.id)!;
        this.List[indexlis] = this._valueList[indexva] = data!;
      })
    }
  }




  Delete() {

    if (this._model.id) {

      this.httpApp.DepartmentDelete(this._model.id).Result(data => {
        this.toastr.success('susccesfully deleted');

        let indexva = this._valueList.findIndex(x => x.id == data!.id)!;
        let indexlis = this.List.findIndex(x => x.id == data!.id)!;

        this._valueList.splice(indexva, 1);
        this.List.splice(indexlis, 1);
        this.List = [...this.List]

        this._model = {};
      });

    }


  }


}
