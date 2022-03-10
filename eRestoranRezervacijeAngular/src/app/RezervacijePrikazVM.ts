export class Rezervacije {
  id?: number;
  userName?: string;
  brojOsoba?: string;
  vrijemeRezervacije?:string;
  brojTelefona?:string;
}

export class RezervacijePrikazVM {
  contentType?: any;
  serializerSettings?: any;
  statusCode?: any;
  value?: Rezervacije[];
}
