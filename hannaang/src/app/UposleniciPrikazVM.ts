export class Uposlenik {
  id?: number;
  ime?: string;
  prezime?: string;
  imePrezime?:string;
  titula?:string;
  restoran?:string;
  restoranID?: number;
}

export class UposleniciPrikazVM {
  contentType?: any;
  serializerSettings?: any;
  statusCode?: any;
  value?: Uposlenik[];
}
