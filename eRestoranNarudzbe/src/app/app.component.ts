import {Component, OnInit} from '@angular/core';
import {NarudzbePrikazVM, Value} from './narudzbe-prikaz-vm';
import {HttpClient} from '@angular/common/http';
import {Myconfig} from './MyConfig';
import {of} from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'eRestoranNarudzbe';
  prikazVM: NarudzbePrikazVM = null;
  trazi: string = null;
  constructor(private http: HttpClient) {
  }
  ngOnInit() {
    this.http.get<NarudzbePrikazVM>(Myconfig.webAppUrl + '/Narudzbe/Index').subscribe((result) => {
      this.prikazVM = result;
    });
  }
  deletePost(id: any) {
    const endPoints = '/Narudzbe/ObrisiAngular/';
    this.http.delete<NarudzbePrikazVM>(Myconfig.webAppUrl + endPoints + id)
      .subscribe(data => {
      this.prikazVM = data;
    });
  }
  onSearch(event: any): void {
    this.trazi = event.target.value;
  }
   getStudenti(): Array<Value>{
      if (this.trazi == null) {
        return this.prikazVM.value;
      }
      return this.prikazVM.value.filter(x => x.nazivJela.toLowerCase().startsWith(this.trazi.toLowerCase()));
   }
}
