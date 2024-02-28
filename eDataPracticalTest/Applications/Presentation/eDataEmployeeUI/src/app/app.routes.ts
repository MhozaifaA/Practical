import { Routes } from '@angular/router';
import { LayoutComponent } from './pages/shared/layout/layout.component';
import { LoginComponent } from './pages/security/login/login.component';
import { EmployeeComponent } from './pages/employee/employee.component';
import { DepartmentComponent } from './pages/department/department.component';
import { MyCVComponent } from './pages/my-cv/my-cv.component';

export const routes: Routes = [

  {
    path: 'login',
    children: [
      { path: '', component: LoginComponent }
    ]
  },

  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: EmployeeComponent },
      { path: 'employees', component: EmployeeComponent },
      { path: 'department', component: DepartmentComponent },
      { path: 'mycv', component: MyCVComponent },
    ]
  }

];
