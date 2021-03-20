import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { Message } from 'primeng/primeng';
import { NotificationsService } from 'angular2-notifications';
export class AlertMessage {
    public show: boolean;
    public message: string;
}

@Injectable()
export class AlertService {
    public alertStatus: BehaviorSubject<AlertMessage> = new BehaviorSubject<AlertMessage>({ show: false, message: null });

    constructor(public notificationService: NotificationsService) {
    }

    showAlert(isShow: boolean, msg: string) {
        let alertObj: AlertMessage = { show: isShow, message: msg };
        this.alertStatus.next(alertObj);
        this.notificationService.success("asd", "mensaje", {
            timeOut: 5000,
            showProgressBar: true,
            pauseOnHover: false,
            clickToClose: false,
            maxLength: 10
        });
    }
}