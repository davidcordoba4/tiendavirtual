import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServicioBaseService } from '../../shared/data-services/servicio-base.Service';
import { NotificacionesMensajeService } from "app/tiendavirtual/shared/notificaciones/notificacionesmensajes.service";
import { Router } from '@angular/router';
import { Orders } from '../modelo/orders';

@Injectable()
export class TiendaVirtualService extends ServicioBaseService<Orders> {
    baseURL: string;
    ordenResumen: Orders;

    constructor(http: HttpClient, public notificacionesMensajeService: NotificacionesMensajeService,router:Router) {
        super(http, notificacionesMensajeService,router);
    }

    obtenerOrdenes(): Promise<any[]> {
        this.EndPoint = "/api/tiendavirtual/obtenerordenestienda";
        return super.TraerListaAnonima();
    }

    ObtenerOrdenTienda(id: number): Promise<any> {
        this.EndPoint = "/api/tiendavirtual/obtenerordentienda";
        return super.TraerAnonima(id);
    }

    CrearOrdenPedido(ordenPedido: Orders): Promise<any> {
        this.EndPoint = "/api/tiendavirtual/crearordenpedido";
        return super.TraerAnonima(ordenPedido);
    }
}