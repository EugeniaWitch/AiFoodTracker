"use client";

import { type ReactNode, useEffect } from "react";
import styles from "./ProductModal.module.css";

type ProductModalProps = {
    title: string;
    isOpen: boolean;
    onClose: () => void;
    children: ReactNode;
}

export function ProductModal({
    title,
    isOpen,
    onClose,
    children
}: ProductModalProps){
    useEffect(() => {
        if (!isOpen){
            return;
        }

        function handleKeyDown(event: KeyboardEvent){
            if (event.key === "Escape"){
                onClose();
            }
        }

        document.addEventListener("keydown",handleKeyDown);
        document.body.style.overflow = "hidden";

        return () => {
            document.removeEventListener("keydown",handleKeyDown);
            document.body.style.overflow = "";
        };
    }, [isOpen, onClose]);

    if (!isOpen){
        return null;
    }

    return (
        <div className={styles.overlay} onMouseDown={onClose}>
            <section
                className={styles.modal}
                role="dialog"
                aria-modal="true"
                
                aria-labelledby="product-modal-title"
                onMouseDown={(event) => event.stopPropagation()}>
                <header className={styles.header}>
                    <h2 id="product-modal-title"
                        className={styles.title}>
                        {title}
                    </h2>
                    <button 
                        type="button"
                        className={styles.closeButton}
                        onClick={onClose}
                        aria-label="Закрыть окно">
                            x
                    </button>
                </header>

                <div className={styles.content}>
                    {children}
                </div>
            </section>
        </div>
    )
}