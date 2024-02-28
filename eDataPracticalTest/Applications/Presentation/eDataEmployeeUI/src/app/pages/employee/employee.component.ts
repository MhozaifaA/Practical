import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpAppService } from '../../core/services/http-app.service';
import { PaginatorComponent } from '../shared/components/paginator/paginator.component';
import { FormsModule, NgForm } from '@angular/forms';
import { EmployeeDto, EmployeeStatus } from '../../core/models/EmployeeDto';
import { CommonModule } from '@angular/common';
import { RegulatorComponent } from '../shared/components/regulator/regulator.component';
import { ToastrService } from 'ngx-toastr';
import { DepartmentDto } from '../../core/models/DepartmentDto';
import { Select2Module, Select2Option } from 'ng-select2-component';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, Select2Module , FormsModule, PaginatorComponent, RegulatorComponent],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css',
})
export class EmployeeComponent implements OnInit {
  isLoaded = true;
  List: EmployeeDto[] = [];
 
  _valueList: EmployeeDto[] = [];
  _model: EmployeeDto = {};

  isAdd = true;

  Departments: Select2Option[] = [];


  constructor(private httpApp: HttpAppService, private toastr: ToastrService,
    private changeDetectorRef: ChangeDetectorRef) {

  }

  ngOnInit(): void {

    this.httpApp.DepartmentList().Result((data) => {
      this.Departments = data!.
        map(x => ({ value: (x.id ?? ''), label: (x.name ?? '') }));;
    });

    this.httpApp.EmployeeList().Result((data) => {
      if (data)
        this.List = data!;
    });
  }

  StatusToValue(i: any) {
      return EmployeeStatus[i]
  }

  StatusToList = Object.keys(EmployeeStatus)
    .filter(value => isNaN(Number(value)) === false)
    .map((key, i) => ({ key: key, value: EmployeeStatus[i] }));

  value(e: EmployeeDto[]) {
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

    this._model.status = Number(this._model.status)

    if (this.isAdd) {


      this.httpApp.EmployeeAdd(this._model).Result(data => {
        this.toastr.success('susccesfully added');

        data!.departmentName = this.Departments.find(x => x.value == data!.departmentId)?.label

        this.List = [data!, ...this.List];//not always, when desc will be last 
        this._model = {};

      }).Error(e => this.toastr.error(e.message))
    } else {
      this.httpApp.EmployeeEdit(this._model).Result(data => {
        this.toastr.success('susccesfully edited');

        data!.departmentName = this.Departments.find(x => x.value == data!.departmentId)?.label


        let indexva = this._valueList.findIndex(x => x.id == data!.id)!;
        let indexlis = this.List.findIndex(x => x.id == data!.id)!;
        this.List[indexlis] = this._valueList[indexva] = data!;
      }).Error(e => this.toastr.error(e.message))
    }
  }




  Delete() {

    if (this._model.id) {

      this.httpApp.EmployeeDelete(this._model.id).Result(data => {
        this.toastr.success('susccesfully deleted');

        let indexva = this._valueList.findIndex(x => x.id == data!.id)!;
        let indexlis = this.List.findIndex(x => x.id == data!.id)!;

        this._valueList.splice(indexva, 1);
        this.List.splice(indexlis, 1);
        this.List = [...this.List]

        this._model = {};
      }).Error(e => this.toastr.error(e.message));

    }


  }


}
