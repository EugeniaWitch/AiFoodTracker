"use client";

import { type FormEvent, useState } from "react";
import { useRouter } from "next/navigation";
import { loginUser } from "@/shared/api/auth";
import styles from "./page.module.css";
import { saveAuthToken } from "@/shared/lib/authToken";

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

            saveAuthToken(response.token);
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
        <main className={styles.page}>
            <div className={styles.login}>
            <h1 className={styles.title}>Вход</h1>
            <form onSubmit={handleSubmit} className={styles.form}>
                <div className={styles.field}>
                    <label htmlFor="email">Email</label>
                    <input id="email"
                        type = "email"
                        value={email}
                        required
                        autoComplete="email"
                        onChange={(event) => {setEmail(event.target.value);
                                                setError("");
                        }}
                        className={styles.input}></input>
                </div>
                <div className={styles.field}>
                    <label htmlFor="password">Пароль</label>
                    <input id="password"
                        type="password"
                        value={password}
                        required
                        autoComplete="current-password"
                        onChange={(event) => {setPassword(event.target.value);
                                                setError("");
                        }}
                        className={styles.input}></input>
                </div>

                {error && <p className={styles.error} role="alert">{error}</p>}

                <button type="submit" className={styles.button} 
                    disabled={isLoading}>{isLoading ? "Входим..":"Войти"}</button>
            </form></div>
        </main>
    );
}