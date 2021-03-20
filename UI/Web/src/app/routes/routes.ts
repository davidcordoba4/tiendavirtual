import { LayoutComponent } from '../layout/layout.component';

export const routes = [
    {
        path: '',  component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'ordenes', pathMatch: 'full' },
            { path: 'ordenes', loadChildren: () => import('../tiendavirtual/ordenes/ordenes.module').then(m => m.OrdenesModule) }
        ]
    },
    // Not found
    { path: '**', redirectTo: 'ordenes' }

];