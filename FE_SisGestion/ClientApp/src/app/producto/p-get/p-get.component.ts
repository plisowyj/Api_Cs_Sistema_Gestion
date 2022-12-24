import { Component, OnInit } from '@angular/core';
import { Producto } from '../../modelos/producto';
import { ApiService } from '../../services/api/api.service';
import { LoaderService } from '../../services/loader.service';
import { global } from '../../services/global';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-p-get',
  templateUrl: './p-get.component.html',
  providers: [ApiService]
})
export class PGetComponent implements OnInit {
  public producto?: Producto;
  public status: string = '';
  public pid: any = null;

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _route: ActivatedRoute, private _router: Router) {
    this._route.params.subscribe(params => {
      this.pid = +params['id'];
      
    })
  }

  ngOnInit(): void {
    this._ls.signal.emit(true);
    this._apiService.UnProducto(this.pid).subscribe(
      response => {
        this.status = 'success';
        this.producto = response;

        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        this._ls.signal.emit(false);

        global.messagePopup("Se producto un Error: ", "No se obtuvo el producto", "error", "Aceptar");
      }
    );
  }

  goBack() {
    this._router.navigate(['/vlist']);
  }
}
