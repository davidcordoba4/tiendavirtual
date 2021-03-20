import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'notificacionesmensajes',
    templateUrl: 'notificacionesmensajes.component.html'
})

export class NotificacionesMensajesComponent implements OnInit {
    public options = {
        position: ['bottom', 'right'],
        timeOut: 5000,
        lastOnBottom: true,
        showProgressBar: true,
        preventDuplicates: true,
        pauseOnHover: true
    };

    constructor() { }

    ngOnInit() { }
}