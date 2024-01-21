import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  opstinePodaci:any;
  odabraniStudent: any;
  defaultOpstinaId: number;
  preuzmiPodatke: string=' ';
  studentZaBrisanje: any ;
  isVidljivoObrisi: boolean;
  dodajStudenta:string ="Dodaj Studenta";
  editStudenta:string="Uredi Studenta";


  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  fetchStudenti() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll", MojConfig.http_opcije()).subscribe((x:any)=>{
      this.studentPodaci = x;
      this.defaultOpstinaId = x[0].opstina_rodjenja_id;

    });

  }
  fetchOpstine() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Opstina/GetByAll", MojConfig.http_opcije()).subscribe(x=>{
      this.opstinePodaci = x;
    });
  }

  ngOnInit(): void {
    this.fetchStudenti();
    this.fetchOpstine();
  }
  pretrazi(){
    if(this.studentPodaci != null){
    return this.studentPodaci.filter((x:any)=>
      (!this.filter_ime_prezime ||
      (x.ime + " " + x.prezime).startsWith(this.ime_prezime)
      || (x.prezime + " " + x.ime).startsWith(this.ime_prezime)
    )
    &&( !this.filter_opstina ||
      (x.opstina_rodjenja.description).startsWith(this.opstina))
    )
    }
    else
      return this.studentPodaci;
  }


  dodajnovi() {
    this.odabraniStudent = {

      id:0,
      ime:this.preuzmiPodatke,
      prezime:" ",
      opstina_rodjenja_id: this.defaultOpstinaId

    }
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+ "/Student/Add", this.odabraniStudent,MojConfig.http_opcije()).subscribe((x:any)=>{
      if (this.odabraniStudent.id == 0){
        porukaSuccess("Novi korisnik uspjesno dodan");
      }
      else {
        porukaSuccess("Korisnik editovan");
      }

      this.fetchStudenti();
      this.odabraniStudent = null;
    })
  }
  obrisi(objekat:any) {
    if (objekat.id != null && objekat.obrisan == true){
      porukaSuccess("uspjesno obrisan");
    }
    else {
      porukaError("Pogresan Korisnik");
      this.isVidljivoObrisi = false;
      return
    }
    this.httpKlijent.put(MojConfig.adresa_servera+ "/Student/Obrisi", objekat,MojConfig.http_opcije()).subscribe((x:any)=>{



      this.fetchStudenti();
      this.studentZaBrisanje = null;
    })
  }

  preuzmiNovePodatke($event: Event) {

    // @ts-ignore
    this.preuzmiPodatke = $event.target.value;
  }

  posalji(b: boolean, id:number) {
    this.studentZaBrisanje={
      id:id,
      obrisan:b
    }
  }

  odaberiPoruku() {
    if (this.odabraniStudent.id != 0){
      return this.editStudenta;
    }
    else
      return this.dodajStudenta;
  }
}
