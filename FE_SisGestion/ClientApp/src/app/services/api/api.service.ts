import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { global } from '../global';
import { Usuario } from '../../modelos/usuario';
import { MVenta, Venta } from '../../modelos/venta';
import { MProducto, Producto } from '../../modelos/producto';
import { MProductoVendido } from '../../modelos/productovendido';

@Injectable()
export class ApiService {
  public url: string;

  constructor(private _http: HttpClient) {
    this.url = global.url;  }

  getIdentity() {
    let identity = localStorage.getItem('identity');
    if (identity && identity != null && identity != undefined && identity != "undefined") {
      return JSON.parse(identity);
    } else { return null; }
  }

  Data(): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'App/Data', { headers: headers });
  }

  UsuarioList(): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Usuarios/list', { headers: headers });
  }
  
  UserLogin(usuario: Usuario): Observable<any> {
    let params = JSON.stringify(usuario);
    var jPar = JSON.parse(params);
    params = JSON.stringify(jPar);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.post(global.url + 'Usuarios/login', params, { headers: headers });
  }

  UserAdd(usuario: Usuario): Observable<any> {
    usuario.nombre = usuario.nombre.toUpperCase();
    usuario.apellido = usuario.apellido.toUpperCase();
    let params = JSON.stringify(usuario);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.post(this.url + 'Usuarios/add', params, { headers: headers });
  }

  UserUpdate(usuario: Usuario): Observable<any> {
    usuario.nombre = usuario.nombre.toUpperCase();
    usuario.apellido = usuario.apellido.toUpperCase();
    let params = JSON.stringify(usuario);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.put(this.url + 'Usuarios/update', params, { headers: headers });
  }

  UnUsuario(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Usuarios/get/' + id, { headers: headers });

  }

  ChangePass(usuario: Usuario): Observable<any> {
    let params = JSON.stringify(usuario);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.put(global.url + 'Usuarios/updatepass', params, { headers: headers });
  }

  UsuarioDelete(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.delete(global.url + 'Usuarios/delete', { headers: headers, body: id });
  }

  VentaList(): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Ventas/list', { headers: headers });
  }

  VentasAdd(venta: MVenta): Observable<any> {
    let params = JSON.stringify(venta);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.post(this.url + 'Ventas/add', params, { headers: headers });
  }

  UnaVenta(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Ventas/get/' + id, { headers: headers });
  }

  VentasUpdate(venta: Venta): Observable<any> {
    let params = JSON.stringify(venta);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.put(this.url + 'Ventas/update', params, { headers: headers });
  }

  VentaDelete(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.delete(global.url + 'Ventas/delete', { headers: headers, body: id });
  }

  ProductosVendidos(idventa: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'ProductosVendidos/list/' + idventa, { headers: headers });
  }

  ProductoList(): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Productos/list', { headers: headers });
  }
    
  UnProducto(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.get(global.url + 'Productos/get/' + id, { headers: headers });
  }

  ProductoUpdate(producto: Producto): Observable<any> {
    let params = JSON.stringify(producto);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.put(this.url + 'Productos/update', params, { headers: headers });
  }

  ProdcutoAdd(producto: MProducto): Observable<any> {
    let params = JSON.stringify(producto);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.post(this.url + 'Productos/add', params, { headers: headers });
  }

  ProductoDelete(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.delete(global.url + 'Productos/delete', { headers: headers, body: id });
  }

  ProductoItemDelete(id: number): Observable<any> {
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.delete(global.url + 'ProductosVendidos/delete', { headers: headers, body: id });
  }

  ProdcutoVendidoAdd(producto: MProductoVendido): Observable<any> {
    let params = JSON.stringify(producto);
    let headers = new HttpHeaders().set('content-type', 'application/json');
    return this._http.post(this.url + 'ProductosVendidos/add', params, { headers: headers });
  }
}

