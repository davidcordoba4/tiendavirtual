import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificacionesMensajeService } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.service';
import { NotificacionesMensajesComponent } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.component';
import { NotificationsService, SimpleNotificationsModule } from 'angular2-notifications';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [ NotificacionesMensajesComponent ],
    providers: [ NotificacionesMensajeService, NotificationsService ],
    exports: [ NotificacionesMensajesComponent, SimpleNotificationsModule ]
})
export class NotificacionesMensajesModule { }