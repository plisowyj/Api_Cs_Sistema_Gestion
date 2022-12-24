import { Component, OnInit } from '@angular/core';
import { MProducto } from '../../modelos/producto';
import { Usuario } from '../../modelos/usuario';
import { ApiService } from '../../services/api/api.service';
import { LoaderService } from '../../services/loader.service';
import { global } from '../../services/global';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-p-edit',
  templateUrl: './p-edit.component.html',
  providers: [ApiService]
})
export class PEditComponent implements OnInit {
  public status: string = '';
  public producto: MProducto = new MProducto(0, '',0, 0, 0, 0, 0, '');
  public titulo: string = "Modificar Producto";
  public boton: string = "Modificar";
  public pid: any = null;
  public usuarios: Usuario[] = [];

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _route: ActivatedRoute, private _router: Router) {
    this._route.params.subscribe(params => {
      this.pid = +params['id'];
    });

    this._ls.signal.emit(true);
    this._apiService.UnProducto(this.pid).subscribe(
      response => {
        this.producto = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo recuperar Producto!", "Intente nuevamente.", "warning", "Volver");
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

    //conseguir objeto completo del user
    this._apiService.ProductoUpdate(this.producto).subscribe(
      response => {
        global.messagePopup("Actualizado!", "Has modificado tu datos.", "success", "Aceptar");
        this._ls.signal.emit(false);
        this._router.navigate(['/plist']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo Actualizar!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }

  UserChange(value: any) {
    this.producto.idUsuario = value;

  }
}
