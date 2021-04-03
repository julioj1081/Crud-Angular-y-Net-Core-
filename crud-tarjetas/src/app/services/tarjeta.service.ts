import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// despues de crear la api creamos el servicio en angular
//C:\Users\julio\Desktop\Lista-Angular\crud-tarjetas>ng g s services/tarjeta
@Injectable({
  providedIn: 'root'
})
export class TarjetaService {
  private myApiUrl = 'https://localhost:44318/';
  private myApiUrl2 = 'api/Tarjeta/';
  //injectable en constructor
  constructor(private http: HttpClient) { }

  getListarTarjetas() :Observable<any>{
    //1:31:49
    return this.http.get(this.myApiUrl + this.myApiUrl2);
  }

  deleteTarjeta(id:number) :Observable<any>{
    return this.http.delete(this.myApiUrl + this.myApiUrl2 + id);
  }

  saveTarjeta(tarjeta: any): Observable<any>{
    return this.http.post(this.myApiUrl + this.myApiUrl2, tarjeta);
  }
  updateTarjeta(id: number, tarjeta:any): Observable<any>{
    return this.http.put(this.myApiUrl + this.myApiUrl2 + id, tarjeta);
  }
}
