import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OperationResult } from '../helper/OperationResult';
import * as config from '../appsettings';
import { AccessTokenDto } from '../models/AccessTokenDto';
import { LoginDto } from '../models/LoginDto';
import { EmployeeDto } from '../models/EmployeeDto';
import { StorageService } from './storage.service';
import { DepartmentDto } from '../models/DepartmentDto';




const httpOptions = {

  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'jwt-token'
    //'Access-Control-Allow-Origin': '*',
    //'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
    //'Access-Control-Allow-Headers' : 'Content-Type, x-requested-with'
  })
};


@Injectable({
  providedIn: 'root'
})
export class HttpAppService {

  constructor(private http: HttpClient, private storage: StorageService) {
  }



  Login(body: LoginDto): OperationResult<AccessTokenDto> {
    return this._Post(config.LoginUrl, body);
  }


  EmployeeList(): OperationResult<EmployeeDto[]> {
    return this._Get(config.EmployeeListUrl);
  }

  EmployeeAdd(body: EmployeeDto): OperationResult<EmployeeDto> {
    return this._Post(config.EmployeeAddUrl, body);
  }
  EmployeeEdit(body: EmployeeDto): OperationResult<EmployeeDto> {
    return this._Put(config.EmployeeModifyUrl, body);
  }

  EmployeeDelete(id: string): OperationResult<EmployeeDto> {
    return this._Delete(config.EmployeeDeleteUrl + '/' + id);
  }


  DepartmentList(): OperationResult<DepartmentDto[]> {
    return this._Get(config.DepartmentFetchUrl);
  }
  DepartmentAdd(body: DepartmentDto): OperationResult<DepartmentDto> {
    return this._Post(config.DepartmentAddUrl, body);
  }
  DepartmentEdit(body: DepartmentDto): OperationResult<DepartmentDto> {
    return this._Put(config.DepartmentModifyUrl, body);
  }

  DepartmentDelete(id: string): OperationResult<DepartmentDto> {
    return this._Delete(config.DepartmentDeleteUrl + '/' + id);
  }


  //without Interceptor

  _Put<T>(url: string, body: any, role?: string | null, action?: (res: T) => void): OperationResult<T> {
    let operation = new OperationResult<T>(this);

    httpOptions.headers = httpOptions.headers.set('Authorization', "Bearer " + this.storage.GetAccessToken());
    this.http.put<T>(
      url, body, httpOptions).subscribe(resp => {
        if (action) {
          action(resp);
        }
        operation.handleResult(resp)
      }, err => {

        operation.handleError(err);
      });

    return operation;
  }

  _Post<T>(url: string, body: any, role?: string | null, action?: (res: T) => void, blob: boolean = false): OperationResult<T> {
    let operation = new OperationResult<T>(this);


    let _httpOptions = { ...httpOptions };
    if (body instanceof FormData) {
      _httpOptions = {
        headers: new HttpHeaders({
          'Authorization': "Bearer " + this.storage.GetAccessToken()
        })
      }
    }

    if (blob) {
      _httpOptions = <any>{
        headers: _httpOptions.headers, observe: 'response',
        responseType: 'blob'
      }
    }


    this.http.post<T>(
      url, body, _httpOptions).subscribe(resp => {
        if (action) {
          action(resp);
        }
        operation.handleResult(resp)
      }, err => {

        operation.handleError(err);
      });

    return operation;
  }

  _Get<T>(url: string, role?: string | null, action?: (res: T) => void, blob: boolean = false): OperationResult<T> {
    let operation = new OperationResult<T>(this);


    httpOptions.headers = httpOptions.headers.set('Authorization', "Bearer " + this.storage.GetAccessToken());
    let _httpOptions = { ...httpOptions };
    if (blob) {
      _httpOptions = <any>{
        headers: httpOptions.headers, observe: 'response',
        responseType: 'blob'
      }
    }

    this.http.get<T>(
      url, _httpOptions).subscribe(resp => {
        if (action) {
          action(resp);
        }
        operation.handleResult(resp)
      }, err => {

        operation.handleError(err);
      });

    return operation;
  }

  _Delete<T>(url: string, role?: string | null, action?: (res: T) => void): OperationResult<T> {
    let operation = new OperationResult<T>(this);

    httpOptions.headers = httpOptions.headers.set('Authorization', "Bearer " + this.storage.GetAccessToken());
    this.http.delete<T>(
      url, httpOptions).subscribe(resp => {
        if (action) {
          action(resp);
        }
        operation.handleResult(resp)
      }, err => {

        operation.handleError(err);
      });

    return operation;
  }



}
