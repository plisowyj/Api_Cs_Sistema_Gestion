export interface Producto {
  id: number;
  descripciones: string;
  costo: number;
  precioVenta: number;
  stock: number;
  idUsuario: number;
  activo: number;
  apeNomUsuario: string;
}


export class MProducto {
  constructor(
    public id: number,
    public descripciones: string,
    public costo: number,
    public precioVenta: number,
    public stock: number,
    public idUsuario: number,
    public activo: number,
    public apeNomUsuario: string) { }
}

