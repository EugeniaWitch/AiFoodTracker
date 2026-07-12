"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getCurrentUser } from "@/shared/api/auth";
import type { CurrentUserResponse } from "@/shared/types/auth";

export default function ProfilePage(){
    const router = useRouter();

    const [user, setUser] = useState<CurrentUserResponse| null >(null);
    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function loadUser() {
            const token = localStorage.getItem("token");
            if (!token){
                router.push("/login");
                return;
            }
            try {
                const currentUser = await getCurrentUser(token);
                setUser(currentUser);
            } catch {
                localStorage.removeItem("token");
                setError("Сессия истекла или токен недействителен");
                router.push("/login");
            } finally {
                setIsLoading(false);
            }
        }
        loadUser();
    }, [router]);

    function handleSubmit(){
        localStorage.removeItem("token");
        router.push("/login");
    }

    if (isLoading){
        return <main>Загрузка...</main>;
    }
    if (error){
        return <main>{error}</main>;
    }
    if (!user){
        return <main>Пользователь не найден</main>;
    }

    return(
        <main>
            <h1>Профиль</h1>

            <p>ID: {user.userId}</p>
            <p>Имя: {user.name}</p>
            <p>Email: {user.email}</p>
            <p>Роль: {user.role}</p>

            <button type="button" onClick={handleSubmit}>Выйти</button>
        </main>
    );
};