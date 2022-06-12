export interface Login {
    id: number;
    username: string;
    password: string;
    isAdmin: boolean;
    isAuthorized?: boolean;
}