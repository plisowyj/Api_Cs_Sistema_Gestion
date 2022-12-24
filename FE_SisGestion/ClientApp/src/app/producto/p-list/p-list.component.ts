import { Component, OnInit } from '@angular/core';
import { Producto } from '../../modelos/producto';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-p-list',
  templateUrl: './p-list.component.html',
  providers: [ApiService]
})
export class PListComponent implements OnInit {
  public productos: Producto[] = [];
  public status: string = '';

  constructor(private _apiService: ApiService, private _ls: LoaderService) { }

  ngOnInit(): void {
    this.CargarList();
  }

  ProductoDelete(id: number) {
    this._ls.signal.emit(true);
    this._apiService.ProductoDelete(id).subscribe(
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
    
  }

  CargarList() {
    this._ls.signal.emit(true);
    this._apiService.ProductoList().subscribe(
      response => {
        this.status = 'success';
        this.productos = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);

        global.messagePopup("Se producto un Error: ", "No se obtuvieron productos", "error", "Aceptar");
      }
    );
  }
}
