import { Component, OnInit, DoCheck } from '@angular/core';
import { ApiService } from './services/api/api.service';
import { LoaderService } from './services/loader.service';
import { Router } from '@angular/router';
import { global } from './services/global';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ApiService]
})
export class AppComponent implements OnInit, DoCheck {
  title = 'Gestión Stock/Venta';
  public identity: any = null;
  public token:any;

  constructor(
    private _apiService: ApiService,
    private _ls: LoaderService,
    private _router: Router  ) {
    
  }

  ngOnInit() {

  }

  ngDoCheck() {
    this.identity = this._apiService.getIdentity();
  }

  logout() {
    localStorage.clear();
    this.identity = null;
    this.token = null;
    this._router.navigate(['/']);
  }

  InputChange(usuario:any) {
    global.messageInput('Ingrese su nueva Contraseña', 'Modificar', '1', this._ls, this._apiService,usuario);
  }
}
