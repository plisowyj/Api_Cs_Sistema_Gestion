import { Component, OnInit } from '@angular/core';
import { Venta } from '../../modelos/venta';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-v-list',
  templateUrl: './v-list.component.html',
  providers: [ApiService]
})
export class VListComponent implements OnInit {
  public ventas: Venta[] = [];
  public status: string = '';

  constructor(private _apiService: ApiService, private _ls: LoaderService) { }

  ngOnInit(): void {
    this.CargarList();
  }

  onSelect(nventa: number): void {
    let sDetalle: string;

    this._ls.signal.emit(true);
    this._apiService.ProductosVendidos(nventa).subscribe(
      response => {
        this.status = 'success';

        sDetalle = "<ol class='list-group list-group-numbered'>";
        for (let item of response) {
          sDetalle += "<li class='list-group-item d-flex justify-content-between align-items-start'>";
          sDetalle += "<div class='ms-2 me-auto'>";
          sDetalle += '<a href =' + "/pitemdelete/" + item.id + ' class="btn btn-danger btn-xs mr-1"><i title="Eliminar" class="fa fa-trash"></i></a><a title="Ver Detalle Producto" class="btn btn-dark btn-xs" href =' + "/plist/" + item.idProducto + "><b>" + item.productoDesc + ' </b> </a>';
          sDetalle += "</div>";
          sDetalle += "<span class='badge bg-warning rounded-pill'> Cant.: #" + item.stock + " </span></li>";

        }
        sDetalle += "</ol>";

        this._ls.signal.emit(false);
        global.messageHtml("Detalle Id.Venta: " + nventa.toString(), "Items", "info", "Volver", sDetalle);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);

        global.messagePopup("Se producto un Error: ", "No se obtuvieron ventas", "error", "Aceptar");
      }
    );

  }

  Delete(id: number) {
    
    this._ls.signal.emit(true);
    this._apiService.VentaDelete(id).subscribe(
        response => {
          this.status = 'success';
          this.CargarList();
        },
        error => {
          console.log(error);

          this.status = 'error';
          global.messagePopup("Se produjo un Error: ", "Se produjo un error al eliminar.", "error", "Aceptar");
        }
      );
    
  }

  CargarList() {
    this._ls.signal.emit(true);
    this._apiService.VentaList().subscribe(
      response => {
        this.status = 'success';
        this.ventas = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);
      }
    );
  }

  EliminarItem(id: number) {
    this._ls.signal.emit(true);
    this._apiService.ProductoItemDelete(id).subscribe(
      response => {
        this.status = 'success';
        this._ls.signal.emit(false);
        this.CargarList();
      },
      error => {
        this._ls.signal.emit(false);
        this.status = 'error';
        global.messagePopup("Se produjo un Error: ", "Error al eliminar Item.", "error", "Aceptar");
      }
    );

  }
}
