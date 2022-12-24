import { Component, OnInit } from '@angular/core';
import { MUsuario } from '../modelos/usuario';
import { ApiService } from '../services/api/api.service';
import { LoaderService } from '../services/loader.service';
import { global } from '../services/global';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './register/userbase.component.html',
  providers: [ApiService]
})
export class ProfileComponent implements OnInit {
  public status: string = '';
  public user: MUsuario;
  public identity: any = null;
  public show: boolean = false;
  public pass: boolean = false;
  public requer: boolean = false;
  public titulo: string = "Mi Perfil";
  public boton: string = "Modificar Perfil Usuario";

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router) {
    this.identity = this._apiService.getIdentity();
    this.user = this.identity;
  }

  ngOnInit(): void {
  }

  onSubmit(form: any) {
    this._ls.signal.emit(true);

    //conseguir objeto completo del user
    this._apiService.UserUpdate(this.user).subscribe(
      response => {
        this._apiService.UnUsuario(this.user.id).subscribe(
          response => {
            localStorage.setItem('identity', JSON.stringify(response));
            this._ls.signal.emit(false);
            global.messagePopup("Actualizado!", "Has modificado tu datos.", "success", "Aceptar");
          },
          error => {
            this.status = 'error';
            global.messagePopup("No se pudo recuperar Usuario!", "Intente nuevamente.", "warning", "Volver");
            this._ls.signal.emit(false);
          }
        );
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo Actualizar!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }
}
