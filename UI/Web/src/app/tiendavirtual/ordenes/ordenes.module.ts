import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DataTableModule } from 'angular2-datatable';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { OrdenesComponent } from './ordenes.component';
import { OrdenListaComponent } from './orden-lista/orden-lista.component';
import { OrdenComponent } from './orden/orden.component';
import { TiendaVirtualService } from './shared/tiendavirtual.service';
import { SharedModule } from "app/shared/shared.module";
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CurrencyMaskConfig, CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
    align: "right",
    allowNegative: true,
    decimal: ",",
    precision: 2,
    prefix: "$ ",
    suffix: "",
    thousands: "."
};

const routes: Routes = [
    { path: '', component: OrdenesComponent },
    { path: 'crear/:esResumen', component: OrdenComponent },
    { path: 'visualizarestado/:esResumen/:id', component: OrdenComponent },
    { path: 'resumen/:esResumen', component: OrdenComponent }
];

@NgModule({
    imports: [
        SharedModule.forRoot(),
        CommonModule,
        ModalModule.forRoot(),
        RouterModule.forChild(routes),
        DataTableModule,
        FormsModule,
        CurrencyMaskModule
    ],
    declarations: [OrdenesComponent, OrdenListaComponent, OrdenComponent],
    providers: [ TiendaVirtualService, { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }],
    exports: [ RouterModule ]
})
export class OrdenesModule { }