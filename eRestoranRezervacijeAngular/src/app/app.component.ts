import { Component,OnInit } from '@angular/core';
import { Myconfig } from './MyConfig';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {RezervacijePrikazVM,Rezervacije} from "./RezervacijePrikazVM";
import {Title} from "@angular/platform-browser";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'belmaangular';
  trazi:string = "";
  editRezervacije:Rezervacije={};

  prikazRezervacija: RezervacijePrikazVM = {};

  constructor(private http: HttpClient,private titleService:Title) {

    this.titleService.setTitle("RezervacijeAngular");
  }

  ngOnInit() {
    this.http.get<RezervacijePrikazVM>(Myconfig.webAppUrl + '/Uposlenik/RezervacijeAngular/Index').subscribe((result) => {
      this.prikazRezervacija.value = result.value;
      console.log(this.prikazRezervacija);
    });
  }

  getRezervacije(){
    if (this.trazi=="")
      return this.prikazRezervacija.value;
    else
      {
        return this.prikazRezervacija.value?.filter(x=>x.userName?.toLocaleLowerCase().startsWith(this.trazi.toLocaleLowerCase()));
      }
  }
   edit(i:Rezervacije)
   {
     this.editRezervacije=i;
   }
   snimi()
   {
this.http.post(Myconfig.webAppUrl+'/Uposlenik/RezervacijeAngular/Edit', this.editRezervacije, Myconfig.httpOpcije).subscribe(result=>{
});
   }

  onSearch(event: any): void {
    this.trazi = event.target.value;
  }
}


