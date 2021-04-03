import { Component, OnInit } from '@angular/core';
//importar form group
import {FormBuilder, FormGroup, Validators} from '@angular/forms'
//importacion de toastr desde app.module y angular.json "styles"
import { ToastrService } from 'ngx-toastr';
import { TarjetaService } from 'src/app/services/tarjeta.service';

@Component({
  selector: 'app-tarjeta-credito',
  templateUrl: './tarjeta-credito.component.html',
  styleUrls: ['./tarjeta-credito.component.css']
})
export class TarjetaCreditoComponent implements OnInit {

  //creamos un array
  listTarjetas:any[] = [
    // {
    //   titular: 'juan perez',
    //   numeroTarjeta: '123456',
    //   fechaExpiracion: '2020/05/12',
    //   CVV: '503'
    // },
    // {
    //   titular: 'miguel gonzalez',
    //   numeroTarjeta: '1513236',
    //   fechaExpiracion: '2024/12/12',
    //   CVV: '103'
    // }
  ];

  //formularios reactivos importar desde el app.module
  form: FormGroup;
  //cambio de titulo
  accion = "Agregar";
  id: number | undefined;

  constructor( 
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private _tarjetaService : TarjetaService
    ) {
    this.form = this.fb.group({
      titular: ['', Validators.required],
      numeroTarjeta: ['', [Validators.required, Validators.maxLength(16), Validators.minLength(16)]],
      fechaExpiracion: ['', [Validators.required, Validators.maxLength(5), Validators.minLength(5)]],
      CVV:['', [Validators.required, Validators.maxLength(3), Validators.minLength(3)]]
    })
   }

  ngOnInit(): void {
    this.obtenerTarjetas();
  }

  //despues de agregar el servico
  obtenerTarjetas(){
    this._tarjetaService.getListarTarjetas().subscribe(data => {
      console.log(data);
      this.listTarjetas = data;
    }, error => {
      console.log(error);
    })
  }

  guardarTarjeta(){
    const tarjeta: any ={
      titular: this.form.get('titular')?.value,
      numeroTarjeta: this.form.get('numeroTarjeta')?.value,
      fechaExpiracion: this.form.get('fechaExpiracion')?.value,
      CVV: this.form.get('CVV')?.value
    }

    if(this.id == undefined){
      //agregamos tarjetas
    //this.listTarjetas.push(tarjeta);
    this._tarjetaService.saveTarjeta(tarjeta).subscribe(data => {

      this.toastr.success('Guardado con exito', 'Tarjeta registrada',{
        timeOut: 3000,
      });
      this.obtenerTarjetas();
      this.form.reset();
    }, error => {
      this.toastr.error('La tarjeta no se guardo con exito', 'Error fatal!', {
        timeOut: 3000,
      });
    })
    }else{
      tarjeta.id = this.id;
      this._tarjetaService.updateTarjeta(this.id, tarjeta).subscribe(data => {
        this.form.reset();
        this.accion = 'Agregar';
        this.id = undefined;
        this.toastr.info('La tarjeta se modifico con exito', 'Update exitoso!',{
          timeOut: 3000,
        })
        this.obtenerTarjetas();
      }, error => {
        alert("error");
      });
    }
    
  }
  

  EliminarTarjeta(index:number){
    //this.listTarjetas.splice(index, 1);
    this._tarjetaService.deleteTarjeta(index).subscribe(data => {
      this.toastr.error('La tarjeta se elimino con exito', 'Eliminado!', {
        timeOut: 3000,
      });
      this.obtenerTarjetas();
    }, error => {
      this.toastr.error('La tarjeta no se elimino con exito', 'Error fatal!', {
        timeOut: 3000,
      });
    })
  }

  editarTarjeta(tarjeta: any){
    this.accion = "Editar ";
    this.id = tarjeta.id;
    this.form.patchValue({
      titular: tarjeta.titular,
      numeroTarjeta: tarjeta.numeroTarjeta,
      fechaExpiracion: tarjeta.fechaExpiracion,
      CVV: tarjeta.cvv
    });
  }


}
