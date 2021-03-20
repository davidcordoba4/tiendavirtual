import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { Orders } from '../modelo/orders';
import { TiendaVirtualService } from '../shared/tiendavirtual.service';

@Component({
    moduleId: module.id,
    selector: 'orden',
    templateUrl: './orden.component.html',
    styleUrls: ['./orden.component.scss']
})

export class OrdenComponent implements OnInit {
    producto: string = 'Camiseta';
    valor: number = 70000;
    id: number;
    orden: Orders;
    sub: any;
    esVisualizacionEstado: boolean = false;
    esResumen: boolean = false;
    camposSoloLectura: boolean = true;
    estadoPedido: string;
    constructor(
        private location: Location,
        private route: ActivatedRoute,
        public tiendaVirtualService: TiendaVirtualService,
        private router: Router,
        public cdr: ChangeDetectorRef) {
    }

    ngOnInit() {
        this.orden = new Orders();
        this.sub = this.route.params.subscribe(
            params => {
                debugger;
                let id = params['id'] as number;
                this.esResumen = JSON.parse(params['esResumen']) as boolean;
                if (id && !this.esResumen) {
                    this.esVisualizacionEstado = true;
                    this.ObtenerOrdenTienda(id);
                }
                else {
                    this.esVisualizacionEstado = false;
                }
                this.camposSoloLectura = this.esVisualizacionEstado || this.esResumen;
                this.orden = this.esResumen ? JSON.parse(JSON.stringify(this.tiendaVirtualService.ordenResumen)) : this.orden;
                this.tiendaVirtualService.ordenResumen = null;
            });
    }

    ObtenerOrdenTienda(id: number) {
        this.tiendaVirtualService.ObtenerOrdenTienda(id).then(orden => {
            debugger;
            this.orden = orden;
            switch (this.orden.OrderStatus.Status_Description) {
                case "PENDING":
                    this.estadoPedido = "PENDIENTE";
                    break;
                case "FAILED":
                    this.estadoPedido = "FALLO AUTENTICACIÓN";
                    break;
                case "APPROVED":
                    this.estadoPedido = "PAGO APROVADO";
                    break;
                case "REJECTED":
                    this.estadoPedido = "PAGO RECHAZADO";
                    break;
                default:
                    this.estadoPedido = this.orden.OrderStatus.Status_Description;
                    break;
            }
        });
    }

    soloNumeros(event: any) {
        const pattern = /[0-9]/;
        let inputChar = String.fromCharCode(event.charCode);
        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
    }

    continuarResumen() {
        this.tiendaVirtualService.ordenResumen = this.orden;
        this.router.navigate(['/ordenes/resumen', true]);
    }

    continuarPago() {
        this.orden.UrlRaiz = window.location.origin;
        this.tiendaVirtualService.CrearOrdenPedido(this.orden).then(orden => {
            this.orden = orden;
            window.location.href = this.orden.UrlProcesamiento;
        });
    }

    reintentarPago() {
        this.continuarResumen();
    }

    volver() {
        this.router.navigate(['/ordenes']);
    }
}