import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';

@Component({
    selector: 'app-ordenes',
    templateUrl: './ordenes.component.html',
    styleUrls: ['./ordenes.component.scss']
})

export class OrdenesComponent implements OnInit {
    public empresasDatos;

    constructor() {
    }

    ngOnInit() {
   
    }

    public removeItem(item: any) {
        this.empresasDatos = _.filter(this.empresasDatos, (elem) => elem !== item);
    }
}
