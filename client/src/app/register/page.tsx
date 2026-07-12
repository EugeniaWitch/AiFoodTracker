"use client";

import { type FormEvent, useState } from "react";
import { useRouter } from "next/navigation";
import { registerUser } from "@/shared/api/auth";
import styles from "./page.module.css";
import { saveAuthToken } from "@/shared/lib/authToken";

export default function RegisterPage(){
    const router = useRouter();

    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    async function handleSubmit(event:FormEvent<HTMLFormElement>){
        event.preventDefault();

        setError("");
        setIsLoading(true);

        try {
            const response = await registerUser({
                name,
                email,
                password,
            });

            saveAuthToken(response.token);
            router.push("/");
        } catch (error){
            if (error instanceof Error){
                setError(error.message);
            } else {
                setError("Registration failed");
            }}
        finally {
            setIsLoading(false);
        }
    }
        
    return(
        <main className={styles.page}>
            <div className={styles.register}><h1 className={styles.title}>Регистрация</h1>
            <form onSubmit={handleSubmit} className={styles.form}>
                <div className={styles.field}>
                    <label htmlFor="name">Имя</label>
                    <input id="name"
                        type = "text"
                        value={name}
                        required
                        minLength={3}
                        maxLength={20}
                        autoComplete="name"
                        onChange={(event) => {setName(event.target.value); 
                                                setError("");}}
                        className={styles.input}>
                    </input>
                </div>
                <div className={styles.field}>
                    <label htmlFor="email">Email</label>
                    <input id="email"
                        type = "email"
                        value={email}
                        required
                        maxLength={100}
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
                        minLength={6}
                        maxLength={100}
                        autoComplete="new-password"
                        onChange={(event) => {setPassword(event.target.value);
                                                setError("");
                        }}
                        className={styles.input}></input>
                </div>

                {error && <p className={styles.error} role="alert">{error}</p>}

                <button type="submit" className={styles.button} 
                    disabled={isLoading}>{isLoading ? "Регистрируем..":"Зарегистрироваться"}</button>
            </form>
            </div>
        </main>
    );
}