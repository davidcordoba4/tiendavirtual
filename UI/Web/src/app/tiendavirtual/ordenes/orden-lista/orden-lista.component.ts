import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from "@angular/animations";
import { TiendaVirtualService } from "../shared/tiendavirtual.service";
import { ConfirmarService } from 'app/tiendavirtual/shared/confirmar/confirmar';
import * as _ from 'lodash';
import { Orders } from '../modelo/orders';
import { Router } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: 'orden-lista',
    templateUrl: './orden-lista.component.html',
    styleUrls: ['./orden-lista.component.scss'],
    animations: [
        trigger('flyInOut', [
            state('in', style({ opacity: 1, transform: 'translateX(0)' })),
            transition('void => *', [
                style({
                    opacity: 0,
                    transform: 'translateX(-100%)'
                }),
                animate('0.5s ease-in')
            ]),
            transition('* => void', [
                animate('100ms ease-out', style({
                    opacity: 0,
                    transform: 'translateX(100%)'
                }))
            ])
        ])
    ]
})

export class OrdenListaComponent implements OnInit {
    ordenes: Orders[];

    constructor(public tiendaVirtualService: TiendaVirtualService,
        private confirmar: ConfirmarService, private router: Router) {
        this.refrescar();
    }

    ngOnInit() { }
    
    refrescar() {
        this.tiendaVirtualService.obtenerOrdenes().then(ordenes => {
            debugger;
            this.ordenes = ordenes
        });
    }
}