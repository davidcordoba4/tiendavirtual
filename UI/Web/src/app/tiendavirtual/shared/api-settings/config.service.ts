import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable()
export class ConfigService {
    private apiURI: string;
    private urlHost: string;

    constructor() {
        this.apiURI = '';
    }

    obtenerApiURI() {
        return this.apiURI;
    }

    obtenerUrlHost() {
        return this.urlHost = environment.urlHost;
    }
}
