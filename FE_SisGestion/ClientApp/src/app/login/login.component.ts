import { Component, OnInit } from '@angular/core';
import { MUsuario } from '../modelos/usuario';
import { ApiService } from '../services/api/api.service';
import { LoaderService } from '../services/loader.service';
import { global } from '../services/global';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [ApiService]
})
export class LoginComponent implements OnInit {
  public status: string = '';
  public user: MUsuario;
  public identity: string = '';

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router) {
    this.user = new MUsuario(0, '', '', '', '', '', '0');
  }

  ngOnInit(): void {
  }
  onSubmit(form: any) {
    this._ls.signal.emit(true);

    //conseguir objeto completo del user
    this._apiService.UserLogin(this.user).subscribe(
      response => {
        this.identity = response;
        localStorage.setItem('identity', JSON.stringify(this.identity));

        this._ls.signal.emit(false);
        this._router.navigate(['/']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo ingresar!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }
}
