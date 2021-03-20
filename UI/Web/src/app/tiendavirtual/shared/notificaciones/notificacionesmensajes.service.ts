import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { Subject } from 'rxjs';

@Injectable()
export class NotificacionesMensajeService {
    public subject_error: Subject<string> = new Subject();

    constructor(public notificationService: NotificationsService) {
    }

    Exitoso(titulo: string, mensaje: string) {
        setTimeout(x=>{
            this.notificationService.success(titulo, mensaje)
        },100);
    }

    Error(titulo: string, mensaje: string) {
        this.notificationService.error(titulo, mensaje);
    }
    
    ErrorConEspera(titulo: string, mensaje: string) {
        setTimeout(x=>{
            this.notificationService.error(titulo, mensaje);
        },100);
    }

    Informativo(titulo: string, mensaje: string) {
        this.notificationService.info(titulo, mensaje);
    }

    Advertencia(titulo: string, mensaje: string) {
        setTimeout(x=>{
            this.notificationService.warn(titulo, mensaje);
        },100);
    }

    ExitosoConEspera(titulo: string, mensaje: string){
        setTimeout(x=>{
            this.notificationService.success(titulo, mensaje);
        },100);
    }

    InformativoConEspera(titulo: string, mensaje: string){
        setTimeout(x => {
            this.notificationService.info(titulo, mensaje);
        },10);
       
    }
}