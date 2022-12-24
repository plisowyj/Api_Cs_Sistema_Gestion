import { Component, OnInit } from '@angular/core';
import { MUsuario } from '../../modelos/usuario';
import { ApiService } from '../../services/api/api.service';
import { LoaderService } from '../../services/loader.service';
import { global } from '../../services/global';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './userbase.component.html',
  providers: [ApiService]
})
export class RegisterComponent implements OnInit {
  public status: string = '';
  public user: MUsuario;
  public show: boolean = false;
  public pass: boolean = true;
  public requer: boolean = true;
  public titulo: string = "Registro Usuario";
  public boton: string = "Registrar";
  
  constructor(private _apiService: ApiService, private _ls: LoaderService, private _router: Router) {
    this.user = new MUsuario(0, '', '', '', '', '', '1');
  }

  ngOnInit(): void {
  }

  onSubmit(form: any) {
    this._ls.signal.emit(true);

    //conseguir objeto completo del user
    this._apiService.UserAdd(this.user).subscribe(
      response => {
        this._ls.signal.emit(false);
        global.messagePopup("Registro exitoso!", "Ahora puede hacer login.", "success", "Continuar");
        this._router.navigate(['/login']);
      },
      error => {
        this.status = 'error';
        global.messagePopup("No se pudo ingresar!", "Intente nuevamente.", "warning", "Volver");
        this._ls.signal.emit(false);
      }
    );
  }
}
