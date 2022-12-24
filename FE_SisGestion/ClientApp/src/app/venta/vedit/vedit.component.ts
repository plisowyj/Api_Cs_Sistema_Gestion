import { Component, OnInit } from '@angular/core';
import { MVenta } from '../../modelos/venta';
import { Usuario } from '../../modelos/usuario';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vedit',
  templateUrl: './vedit.component.html'
})
export class VeditComponent implements OnInit {
  public status: string = '';
  public titulo: string = 'Agregar Venta';
  public venta: MVenta;
  public boton: string = 'Confirmar';
  public usuarios: Usuario[] = [];

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router) {
    this.venta = new MVenta(0, '', '','');
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

    this._apiService.VentasAdd(this.venta).subscribe(
      response => {
        this._ls.signal.emit(false);
        global.messagePopup("Venta agregada!", "Puede agregar items.", "success", "Continuar");
        this._router.navigate(['/vlist']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo agregar Venta!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }

  UserChange(value: any) {
    this.venta.idUsuario = value;
    
  }
}
