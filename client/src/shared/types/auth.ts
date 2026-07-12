export type RegisterRequest = {
    name: string;
    email: string;
    password: string;
};

export type LoginRequest = {
    email: string;
    password: string;
};

export type AuthResponse = {
    token: string;
    userId: string;
    name: string;
    email: string;
    role: string;
};

export type CurrentUserResponse = {
    userId: string;
    name: string;
    email: string;
    role: string;
}