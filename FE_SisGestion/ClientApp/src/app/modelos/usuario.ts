export interface Usuario {
  id: number;
  nombre: string;
  apellido: string;
  nombreUsuario: string;
  contrasenia: string;
  mail: string;
  activo: string;
}

export class MUsuario {
  constructor(
    public id: number,
    public nombre: string,
    public apellido: string,
    public nombreUsuario: string,
    public contrasenia: string,
    public mail: string,
    public activo: string) { }
}
