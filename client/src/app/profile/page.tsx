"use client";

import { useRouter } from "next/navigation";
import { removeAuthToken } from "@/shared/lib/authToken";
import { useCurrentUser } from "@/features/auth/hooks/useCurrentUser";

export default function ProfilePage(){
    const router = useRouter();
    const { user, error, isLoading } = useCurrentUser();
    
    function handleSubmit(){
        removeAuthToken();
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