import {HttpHeaders} from "@angular/common/http";

export class Myconfig{
  static webAppUrl = 'https://localhost:44342';
  static httpOpcije={ headers:new HttpHeaders({"Contetnt-Type":"application/json"})};
}

