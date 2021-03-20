declare var $: any;

import { Injectable } from '@angular/core';
@Injectable()
export class LoadingService {
    constructor() {
    }

    public showLoading(loadingContent?: any) {
        if (loadingContent) {
            loadingContent.LoadingOverlay('show');
        } else {
            $.LoadingOverlay('show');
        }
    }

    public hideLoading(loadingContent?: any) {
        if (loadingContent) {
            loadingContent.LoadingOverlay('hide');
        } else {
            $.LoadingOverlay('hide');
        }
    }
}