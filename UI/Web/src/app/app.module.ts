import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { LayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { RoutesModule } from './routes/routes.module';
declare var $: any;
import { SimpleNotificationsModule, NotificationsService } from 'angular2-notifications';
import { AlertService } from 'app/tiendavirtual/shared/notificaciones/alert.service';
import { NotificacionesMensajesModule } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        HttpClientModule,
        BrowserAnimationsModule, // required for ng2-tag-input
        CoreModule,
        LayoutModule,
        SharedModule.forRoot(),
        SimpleNotificationsModule.forRoot(),
        RoutesModule,
        SimpleNotificationsModule.forRoot(),
        NotificacionesMensajesModule
    ],
    providers: [NotificationsService, AlertService],
    bootstrap: [AppComponent]
})
export class AppModule { }