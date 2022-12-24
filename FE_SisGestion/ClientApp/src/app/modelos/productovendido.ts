export interface ProductoVendido {
  id: number;
  stock: number;
  idProducto: number;
  idVenta: number;
  productoDesc: string;
}

export class MProductoVendido {
  constructor(
    public id: number,
    public stock: number,
    public idProducto: number,
    public idVenta: number,
    public productoDesc: string) { }
}
