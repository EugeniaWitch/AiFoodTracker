import {
    AuthResponse,
    LoginRequest,
    RegisterRequest,
} from "@/shared/types/auth";

const API_URL = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:4000";

async function requestAuth(
    path: string,
    body: RegisterRequest | LoginRequest
): Promise<AuthResponse>{
    const response = await fetch(`${API_URL}${path}`,{
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
    });

    if (!response.ok) {
        const contentType = response.headers.get("content-type");

        if (contentType?.includes("application/json")) {
            const error = await response.json();
            throw new Error(error?.message ?? `Request failed with status ${response.status}`);
        }

        throw new Error(`Request failed with status ${response.status}`);
    }

    return response.json();
}

export function registerUser(data:RegisterRequest) : Promise<AuthResponse>{
    return requestAuth("/api/auth/register", data);
}

export function loginUser(data:LoginRequest): Promise<AuthResponse>{
    return requestAuth("/api/auth/login", data);
}