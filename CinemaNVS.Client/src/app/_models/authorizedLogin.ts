import { Login } from "./login";

export interface AuthorizedLogin {
    loginResponse: Login;
    jwToken: string;
}