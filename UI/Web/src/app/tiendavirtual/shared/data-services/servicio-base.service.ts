import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ConfigService } from '../api-settings/config.service';
import { NotificacionesMensajeService } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.service';
import { LoadingService } from 'app/tiendavirtual/shared/utilidades/loading/loading.service';
import { Router } from '@angular/router';
declare var $: any;

@Injectable()
export abstract class ServicioBaseService<T> {
    private urlHost: string = '';
    private endPoint: string = '';
    public options: {};
    private storage = window.localStorage;
    configService: ConfigService = new ConfigService();
    loadingService: LoadingService = new LoadingService();
    ocultarLoading: boolean = true;
    constructor(public http: HttpClient,
        public notificacionesMensajeService: NotificacionesMensajeService, public router: Router) {
        this.urlHost = this.configService.obtenerUrlHost();
        this.asignarOpcionesHeaders();
    }

    asignarOpcionesHeaders() {
        this.options = {
            headers: new HttpHeaders()
                .set('Content-Type', 'application/json')
                .set('Accept', 'application/json')
                .set('Access-Control-Allow-Origin', '*')
                .set('Access-Control-Allow-Headers', '*')
                .set('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, DELETE')
                .set('Allow', 'GET, POST, OPTIONS, PUT, DELETE')
        };
    }

    get EndPoint(): string {
        return this.endPoint;
    }

    set EndPoint(endPoint: string) {
        this.endPoint = endPoint;
    }

    TraerListaAnonima(body: any = null) : Promise<any[]> {
        this.loadingService.showLoading();
        this.asignarOpcionesHeaders();
        const bodyString = JSON.stringify(body); // Stringify payload
        return this.http.post(this.urlHost + this.EndPoint, bodyString, this.options).toPromise()
            .then(res => this.extraerDatoPromesa(res, this.notificacionesMensajeService, "", this))
            .catch(err => this.handleErrorPromise(err, this.notificacionesMensajeService, false, this));
    }

    TraerAnonima(body: any = null): Promise<any> {
        this.loadingService.showLoading();
        this.asignarOpcionesHeaders();
        const bodyString = JSON.stringify(body); // Stringify payload
        return this.http.post(this.urlHost + this.EndPoint, bodyString, this.options).toPromise()
            .then(res => this.extraerDatoPromesa(res, this.notificacionesMensajeService, "", this))
            .catch(err => this.handleErrorPromise(err, this.notificacionesMensajeService, false, this));
    }

    private extraerDatoPromesa(res, notificacionesMensajeService?: any, mensaje?: any, componenteActual?: any) {
        if (componenteActual && componenteActual.ocultarLoading) {
            $.LoadingOverlay('hide');
        }
        return res;
    }

    private handleErrorPromise(error, notificacionesMensajeService?: any, imprimirMensajeBadRequest: boolean = false, componenteActual?: any) {
        if (componenteActual && componenteActual.ocultarLoading) {
            $.LoadingOverlay('hide');
        }
        if (error && error.status) {
            if (typeof (notificacionesMensajeService) !== 'undefined') {
                notificacionesMensajeService.Error('Error',
                    error.error.Message || error);
            }
        }
        return Promise.reject(error.error.Message || error);
    }
}
