import { ProductoVendido } from "./productovendido";

export interface Venta {
  id: number;
  comentarios: string;
  idUsuario: any;
  apeNomUsuario?: string;
  ventaProducto?: ProductoVendido;
}

export class MVenta {
  constructor(
    public id: number,
    public comentarios: string,
    public idUsuario: any,
    public apeNomUsuario: string, 
    public ventaProducto?:ProductoVendido) { }
}
