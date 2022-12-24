import { Component, OnInit } from '@angular/core';
import { MVenta } from '../../modelos/venta';
import { Usuario } from '../../modelos/usuario';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-vedit',
  templateUrl: './vedit.component.html'
})
export class VupdateComponent implements OnInit {
  public status: string = '';
  public titulo: string = 'Modificar Venta';
  public venta: MVenta;
  public boton: string = 'Modificar';
  public usuarios: Usuario[] = [];
  public pid: any = null;

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router,private _route: ActivatedRoute) {
    this.venta = new MVenta(0, '', '', '');
    this._route.params.subscribe(params => {
      this.pid = +params['id'];
    });

    this._ls.signal.emit(true);
    this._apiService.UnaVenta(this.pid).subscribe(
      response => {
        this.venta = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo recuperar Venta!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }

  ngOnInit(): void {
    this._apiService.UsuarioList().subscribe(
      response => {
        this.usuarios = response;
      },
      error => {
        global.messagePopup("Se produjo un Error: ", "Lista usuarios no cargada", "error", "Aceptar");
      }
    );
  }

  onSubmit(form: any) {
    this._ls.signal.emit(true);

    this._apiService.VentasUpdate(this.venta).subscribe(
      response => {
        this._ls.signal.emit(false);
        global.messagePopup("Modificación realizada!", "Continue con los items.", "success", "Continuar");
        this._router.navigate(['/vlist']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo modificar Venta!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }

  UserChange(value: any) {
    this.venta.idUsuario = value;

  }
}
