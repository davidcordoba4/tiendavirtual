import { Status } from "./status";

export class Orders {
    Id: number;
    Customer_Name: string;
    Customer_Email: string;
    Customer_Mobile: string;
    Status_Id: number;
    Created_At: Date;
    Updated_At: Date;
    Request_Id: string;
    OrderStatus: Status;
    UrlRaiz: string;
    UrlProcesamiento: string;
    constructor() {
    }
}
