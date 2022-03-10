export class Value {
  narudzbaID: number;
  nazivJela: string;
  ime: string;
  prezime: string;
  datumNarudzbe: Date;
  cijena: number;
  adresa: string;
  telefon: string;
}

export class NarudzbePrikazVM {
  contentType?: any;
  serializerSettings?: any;
  statusCode?: any;
  value: Value[];
}
