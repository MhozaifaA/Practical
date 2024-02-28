import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { StorageService } from '../../../core/services/storage.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent implements OnInit {
  
  constructor(public Storage: StorageService, private router: Router) {
    if (!this.Storage.IsAuthorize()) {
      this.Storage.Flush();
      this.router.navigateByUrl("login")
    }
  }

  ngOnInit(): void {
   
  }


}
