import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api/api.service';
import { global } from '../../services/global';
import { LoaderService } from '../../services/loader.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-pitem-delete',
  templateUrl: './pitem-delete.component.html'
})
export class PitemDeleteComponent implements OnInit {
  public pitem: any = null;

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router, private _route: ActivatedRoute) {
    this._route.params.subscribe(params => {
      this.pitem = +params['id'];
    });

    this._apiService.ProductoItemDelete(this.pitem).subscribe(
      response => {
        global.messagePopup("Item Eliminado!", "Puede continuar con las ventas.", "success", "Aceptar");
      },
      error => {
        global.messagePopup("Se produjo un Error!", "Error al eliminar Item.", "error", "Aceptar");
      }
    );
    this._router.navigate(['/vlist']);
  }

  ngOnInit(): void {
    
  }

}
