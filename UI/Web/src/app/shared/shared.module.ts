import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { RatingModule } from 'ngx-bootstrap/rating';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { TreeTableModule } from 'primeng/components/treetable/treetable';
import { ContextMenuModule } from 'primeng/primeng';
import { DialogModule } from 'primeng/primeng';
import { PanelModule } from 'primeng/primeng';
import { FieldsetModule } from 'primeng/primeng';
import { ToolbarModule } from 'primeng/primeng';
import { LightboxModule, CalendarModule, EditorModule } from 'primeng/primeng';
import {ImageZoomModule} from 'angular2-image-zoom';
import { MultiselectDropdownModule } from 'angular-2-dropdown-multiselect';
import { NotificationsService } from 'angular2-notifications';
import { NotificacionesMensajeService } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.service';
import { NotificacionesMensajesModule } from 'app/tiendavirtual/shared/notificaciones/notificacionesmensajes.module';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { ConfigService } from 'app/tiendavirtual/shared/api-settings/config.service';
import {AutoCompleteModule} from 'primeng/primeng';
import { ConfirmarService } from 'app/tiendavirtual/shared/confirmar/confirmar';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        TranslateModule.forRoot(),
        AccordionModule.forRoot(),
        AlertModule.forRoot(),
        ButtonsModule.forRoot(),
        CarouselModule.forRoot(),
        CollapseModule.forRoot(),
        BsDropdownModule.forRoot(),
        ModalModule.forRoot(),
        PaginationModule.forRoot(),
        ProgressbarModule.forRoot(),
        RatingModule.forRoot(),
        TabsModule.forRoot(),
        TimepickerModule.forRoot(),
        TooltipModule.forRoot(),
        TypeaheadModule.forRoot(),
        TreeTableModule,
        ContextMenuModule,
        DialogModule,
        PanelModule,
        FieldsetModule,
        ToolbarModule,
        LightboxModule,
        ImageZoomModule,
        MultiselectDropdownModule,
        NotificacionesMensajesModule,
        CalendarModule,
        CurrencyMaskModule,
        EditorModule,
        
    ],
    providers: [
        NotificationsService,
        NotificacionesMensajeService,
        ConfigService,
        ConfirmarService
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        RouterModule,
        AccordionModule,
        AlertModule,
        ButtonsModule,
        CarouselModule,
        CollapseModule,
        BsDropdownModule,
        ModalModule,
        PaginationModule,
        ProgressbarModule,
        RatingModule,
        TabsModule,
        TimepickerModule,
        TooltipModule,
        TypeaheadModule,
        TreeTableModule,
        ContextMenuModule,
        DialogModule,
        PanelModule,
        FieldsetModule,
        ToolbarModule,
        LightboxModule,
        ImageZoomModule,
        MultiselectDropdownModule,
        NotificacionesMensajesModule,
        CalendarModule,
        CurrencyMaskModule,
        EditorModule
    ]
})

export class SharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule
        };
    }
}