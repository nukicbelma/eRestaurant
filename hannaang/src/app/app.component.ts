import { Component,OnInit } from '@angular/core';
import { Myconfig } from './MyConfig';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UposleniciPrikazVM,Uposlenik} from "./UposleniciPrikazVM";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'hannaang';
  trazi:string = "";
  urediUposlenik:Uposlenik={};

  prikazUposlenika: UposleniciPrikazVM = {};

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.http.get<UposleniciPrikazVM>(Myconfig.webAppUrl + '/Admin/UposleniciAngular/Index').subscribe((result) => {
      this.prikazUposlenika.value = result.value;
      console.log(this.prikazUposlenika);
    });
  }

  getUposlenici(){
    if (this.trazi=="")
      return this.prikazUposlenika.value;
    else
      {
        return this.prikazUposlenika.value?.filter(x=>x.ime?.toLocaleLowerCase().startsWith(this.trazi.toLocaleLowerCase()));
      }


  }
   uredi(i:Uposlenik)
   {
     this.urediUposlenik=i;
   }
   snimi()
   {

this.http.post(Myconfig.webAppUrl+'/Admin/UposleniciAngular/SnimiAngular',this.urediUposlenik,Myconfig.httpOpcije).subscribe(result=>{

});
   }

  onSearch(event: any): void {
    this.trazi = event.target.value;
  }
}


