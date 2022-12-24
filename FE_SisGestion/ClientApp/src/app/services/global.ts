import Swal from 'sweetalert2';
import { ApiService } from '../services/api/api.service';
import { LoaderService } from '../services/loader.service';

export var global={
  url: 'https://localhost:7175/api/v1/',

	
	/* Iconos: success | error | question | info | warning */
	messagePopup(titulo:string,texto:string,icon:any, button:string){
		Swal.fire({
	            title: titulo,
	            text: texto,
	            icon: icon,
	            confirmButtonText: button
	          })
	},
	
	messageConfirm(titulo:string,texto:string,icon:any){
		Swal.fire({
		  title: titulo,
		  text: texto,
		  icon: icon,
		  showCancelButton: true,
		  confirmButtonText: 'Confirmar',
		  cancelButtonText: 'Cancelar',
		  reverseButtons: true
		}).then((result) => {
		  if (result.isConfirmed) {
		    Swal.fire({
			      text:'Realizado!',
			      icon:'success'
			  	}
		    )
		  } else if (
		    /* Read more about handling dismissals below */
		    result.dismiss === Swal.DismissReason.cancel
		  ) {
		    Swal.fire({
			      text:'Cancelado!',
			      icon:'info'
			  	}
		    )
		  }
		})
	},

	messageHtml(titulo:string,texto:string,icon:any, button:string, html:string){
		Swal.fire({
	            title: titulo,
	            text: texto,
	            icon: icon,
	            confirmButtonText: button,
                html: html
	          })
    },

  messageInput(titulo: string, button: string, action: string, _ls: LoaderService, _apiService: ApiService,usuario:any) {
      Swal.fire({
        title: titulo,
        input: 'password',
        inputAttributes: {
          autocapitalize: 'off'
        },
        showCancelButton: false,
        confirmButtonText: button,
        showLoaderOnConfirm: false,
        preConfirm: (data) => {
          _ls.signal.emit(true);

          usuario.contrasenia = data;

          if (data != "") {
            _apiService.ChangePass(usuario).subscribe(
              response => {

              },
              error => {
                global.messagePopup("No se pudo actualizar!", "Intente nuevamente.", "error", "Volver");
                return "";

              });
            return "ok";
          } else {
            return "";
          }

          
        }
        
      }).then((result) => {
        _ls.signal.emit(false);
        
        if (result.isConfirmed) {
          if (result.value == "ok") {
            global.messagePopup("Cambio exitoso!", "Cierre sesión e ingrese nuevamente.", "success", "Continuar");
          }
        }
      })
    }

};


