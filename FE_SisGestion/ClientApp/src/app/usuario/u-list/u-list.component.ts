import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../modelos/usuario';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-u-list',
  templateUrl: './u-list.component.html',
  providers: [ApiService]
})
export class UListComponent implements OnInit {
  public usuarios: Usuario[] = [];
  public status: string = '';
  public identity: any = null;

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router) {
    this.identity = this._apiService.getIdentity();
  }

  ngOnInit(): void {
    this.CargarList();
  }

  UserDelete(id: number) {
    if (this.identity.id != id) {
      this._ls.signal.emit(true);
      this._apiService.UsuarioDelete(id).subscribe(
        response => {
          this.status = 'success';
          this._ls.signal.emit(false);
          this.CargarList();
        },
        error => {
          this._ls.signal.emit(false);
          this.status = 'error';
          global.messagePopup("Se produjo un Error: ", "Se produjo un error al desactivar.", "error", "Aceptar");
        }
      );
    } else {
      global.messagePopup("No es posible Desactivar", "No puede desactivar el usuario logueado", "info", "Aceptar");
    }
  }

  CargarList() {
    this._ls.signal.emit(true);
    this._apiService.UsuarioList().subscribe(
      response => {
        this.status = 'success';
        this.usuarios = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);

        global.messagePopup("Se producto un Error: ", "No se obtuvieron usuarios", "error", "Aceptar");
      }
    );

  }

}
