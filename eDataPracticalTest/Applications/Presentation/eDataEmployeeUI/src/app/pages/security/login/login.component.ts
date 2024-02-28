import { Component } from '@angular/core';
import { HttpAppService } from '../../../core/services/http-app.service';
import { LoginDto } from '../../../core/models/LoginDto';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { StorageService } from '../../../core/services/storage.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  //default
  model: LoginDto = { userName: "admin", password: "admin", rememberMe: true }

  constructor(private httpApp: HttpAppService, private storage: StorageService,
    private toastr: ToastrService, private router: Router) {
    this.storage.Flush();
  }


  Login(form: NgForm) {
    this.storage.Flush();
    if (form.invalid) {
      return;
    }
  
    this.httpApp.Login(this.model).Result(dto => {
      if (dto) {
        this.storage.SetCurrentAccount(dto);
        if (dto.accessToken) {
          this.storage.SetAccessToken(dto.accessToken);
          this.router.navigateByUrl('');
        }
      }
    }).Error(e => {
      this.toastr.error(e.message);      
    })

  }
}
