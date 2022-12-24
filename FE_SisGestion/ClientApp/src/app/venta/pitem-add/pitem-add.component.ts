import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MProductoVendido } from '../../modelos/productovendido';
import { Producto } from '../../modelos/producto';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-pitem-add',
  templateUrl: './pitem-add.component.html'
})
export class PitemAddComponent implements OnInit {
  public status: string = '';
  public prodVendido: MProductoVendido;
  public pid: number = 0;
  public productos: Producto[] = [];

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _route: ActivatedRoute, private _router: Router) {
    this._route.params.subscribe(params => {
      this.pid = +params['id'];
    });

    this.prodVendido = new MProductoVendido(0, 0, 0, this.pid, '');
  }

  ngOnInit(
  ): void {
    this._apiService.ProductoList().subscribe(
      response => {
        this.productos = response;
      },
      error => {
        global.messagePopup("Se produjo un Error: ", "Lista productos no cargada", "error", "Aceptar");
      }
    );
  }

  onSubmit(form: any) {
    this._ls.signal.emit(true);

    this._apiService.ProdcutoVendidoAdd(this.prodVendido).subscribe(
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

  ProdChange(value: any) {
    this.prodVendido.idProducto = value;

  }
}
