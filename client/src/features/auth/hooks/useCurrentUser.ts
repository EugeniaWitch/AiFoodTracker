"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getCurrentUser } from "@/shared/api/auth";
import { getAuthToken, removeAuthToken } from "@/shared/lib/authToken";
import type { CurrentUserResponse } from "@/shared/types/auth";

export function useCurrentUser(){
    const router = useRouter();

    const [user, setUser] = useState<CurrentUserResponse | null>(null);
    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function loadUser() {
            const token = getAuthToken();

            if (!token){
                router.push("/login");
                return;
            }
            try {
                const currentUser = await getCurrentUser(token);
                setUser(currentUser);
            } catch{
                removeAuthToken();
                setError("Сессия истекла или токен недействителен");
                router.push("/login");
            } finally{
                setIsLoading(false);
            }
        }
        loadUser();
    }, [router]);

    return {
        user,
        error,
        isLoading,
    };
}