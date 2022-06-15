import { Customer } from "./customer";

export interface Login {
    id: number;
    username: string;
    password: string;
    isAdmin: boolean;
    isAuthorized?: boolean;
    customerId: number;
    customerResponse?: Customer;
}