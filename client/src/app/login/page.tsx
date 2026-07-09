"use client";

import { type FormEvent, useState } from "react";
import { useRouter } from "next/navigation";
import { loginUser } from "@/shared/api/auth";

export default function LoginPage(){
    const router = useRouter();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    async function handleSubmit(event:FormEvent<HTMLFormElement>){
        event.preventDefault();

        setError("");
        setIsLoading(true);

        try {
            const response = await loginUser({
                email,
                password,
            });

            localStorage.setItem("token",response.token);
            router.push("/");
        } catch (error){
            if (error instanceof Error){
                setError(error.message);
            } else {
                setError("Login failed");
            }}
        finally {
            setIsLoading(false);
        }
    }
        
    return(
        <main>
            <h1>Вход</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="email">Email</label>
                    <input id="email"
                        type = "email"
                        value={email}
                        onChange={(event) => setEmail(event.target.value)}></input>
                </div>
                <div>
                    <label htmlFor="password">Пароль</label>
                    <input id="password"
                        type="password"
                        value={password}
                        onChange={(event) => setPassword(event.target.value)}></input>
                </div>

                {error && <p>{error}</p>}

                <button type="submit" 
                    disabled={isLoading}>{isLoading ? "Входим..":"Войти"}</button>
            </form>
        </main>
    );
}