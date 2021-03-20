import Swal from 'sweetalert2';
import { Injectable } from '@angular/core';

@Injectable()
export class ConfirmarService {

  constructor() { }

    async showConfirmInfo(message: string, title: string): Promise<boolean> {
        return await Swal.fire({
            title: title,
            text: message,
            allowOutsideClick: false,
            showCancelButton: true,            
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar',
            position: 'top',
            icon: 'info',
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            reverseButtons: true
        }).then((result) => Boolean(result.value));
    }

    async showConfirmDelete(message: string, title: string): Promise<boolean> {
        return await Swal.fire({
            title: title,
            text: message,
            allowOutsideClick: false,
            showCancelButton: true,            
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Aceptar',
            position: 'top',
            icon: 'warning',
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            reverseButtons: true
        }).then((result) => Boolean(result.value));
    }

    showDelete(){
        Swal.fire(
            'Eliminado',
            'Registro eliminado correctamente.',
            'success'
        )
    }

    async showConfirmUpdate(title: string, message: string): Promise<boolean>{
    return await Swal.fire({
        title: title,
        text: message,
        allowOutsideClick: false,
        showCancelButton: true, 
        cancelButtonText: 'No',
        confirmButtonText: 'Si',
        icon: 'warning',
        showClass: {
          popup: 'animated fadeInDown faster'
        },
        hideClass: {
          popup: 'animated fadeOutUp faster'
        },
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        reverseButtons: true
      }).then((result) => Boolean(result.value));
    }

    async showConfirmWarning(message: string, title: string): Promise<boolean> {
        return await Swal.fire({
            title: title,
            text: message,
            allowOutsideClick: false,
            showConfirmButton: true,
            confirmButtonText: 'Aceptar',
            position: 'top',
            icon: 'warning',
            customClass: {
                confirmButton: 'btn btn-primary'
            },
            reverseButtons: true
        }).then((result) => Boolean(result.value));
    }
}
