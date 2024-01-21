import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {dateTimestampProvider} from "rxjs/internal/scheduler/dateTimestampProvider";
import {DatePipe} from "@angular/common";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  podaci: any;
  akademskeGodinePodaci: any;
  pripremiZaDodavanje: any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}
  isVidljivoForma:boolean;
  idStudenta= this.route.snapshot.params["id"];
  isVidljivoOvjeri: boolean;

  pripremi(){
    this.pripremiZaDodavanje={
      student_id: this.idStudenta,
      ime:this.podaci.ime,
      prezime:this.podaci.prezime,
      akademska_godina_id:1,
      godinaStudija:1,
      isObnova:false,
      datumUpisaZimski:new Date(),
      datumOvjere:null,
      evidentirao_korisnik:this.podaci.evidentirao_korisnik,
      cijenaSkolarine:0
    }
  }

  upisiSemestar(s:any){
    for (let p of this.podaci.listaUpisi) {
      if (p.godinaStudija == this.pripremiZaDodavanje.godinaStudija && this.pripremiZaDodavanje.isObnova == false) {
        porukaError("Moguce dodati istu godinu studija samo ako je obnova")
        return
      }
    }
    this.httpKlijent.post(MojConfig.adresa_servera+ "/MaticnaKnjiga/Add", this.pripremiZaDodavanje,MojConfig.http_opcije()).subscribe((x:any)=>{
        porukaSuccess("Zimski Semestar upisan");


      this.fetchMaticnaKnjigaPodaci();
      this.pripremiZaDodavanje = null;
    })
  }
  pripremiZaEdit(idsemestra:number){
  this.pripremiZaDodavanje = {
    id: idsemestra,
    napomena:"",
    datumOvjere:new Date()
  }
}
  ovjeriSemestar(){

    this.httpKlijent.put(MojConfig.adresa_servera+ "/MaticnaKnjiga/Ovjeri", this.pripremiZaDodavanje,MojConfig.http_opcije()).subscribe((x:any)=>{
      porukaSuccess("Semestar ovjeren");
      this.fetchMaticnaKnjigaPodaci();
      this.pripremiZaDodavanje = null;
    })
  }
  fetchAkademskeGodine(){
    this.httpKlijent.get(MojConfig.adresa_servera+ "/AkademskeGodine/GetAll_ForCmb", MojConfig.http_opcije()).subscribe(x=>{
      this.akademskeGodinePodaci = x;
    });
  }

  fetchMaticnaKnjigaPodaci() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/MaticnaKnjiga/GetById?id="+this.idStudenta, MojConfig.http_opcije()).subscribe((x:any)=>{
      this.podaci = x;
    });

  }
  ngOnInit(): void {
    this.fetchMaticnaKnjigaPodaci();
    this.fetchAkademskeGodine();
    this.podaci ={
      ime:""
    }
  }
}
