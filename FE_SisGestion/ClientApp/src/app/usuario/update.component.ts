import { Component, OnInit } from '@angular/core';
import { MUsuario } from '../modelos/usuario';
import { ApiService } from '../services/api/api.service';
import { LoaderService } from '../services/loader.service';
import { global } from '../services/global';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './register/userbase.component.html',
  providers: [ApiService]
})
export class UpdateComponent implements OnInit {
  public status: string = '';
  public user: MUsuario = new MUsuario(0,'','','','','','');
  public identity: any = null;
  public show: boolean = true;
  public pass: boolean = true;
  public requer: boolean = false;
  public titulo: string = "Modificar Usuario";
  public boton: string = "Modificar Datos Usuario";
  public pid: any = null;

  constructor(private _apiService: ApiService, private _ls: LoaderService, private _route: ActivatedRoute,   private _router: Router) {
    this._route.params.subscribe(params => {
      this.pid = +params['id'];
    });
    this.identity = this._apiService.getIdentity();

    this._ls.signal.emit(true);
    this._apiService.UnUsuario(this.pid).subscribe(
      response => {
        this.user = response;
        this._ls.signal.emit(false);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo recuperar Usuario!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }

  ngOnInit(): void {
    
  }

  onSubmit(form: any) {
    this._ls.signal.emit(true);

    //conseguir objeto completo del user
    this._apiService.UserUpdate(this.user).subscribe(
      response => {
        global.messagePopup("Actualizado!", "Has modificado tu datos.", "success", "Aceptar");
        this._ls.signal.emit(false);
        this._router.navigate(['/ulist']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo Actualizar!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }
}
