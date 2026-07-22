"use client";

import { createProduct } from "@/shared/api/products";
import { getAuthToken } from "@/shared/lib/authToken";
import type { CreateProductRequest } from "@/shared/types/product";
import { ProductForm } from "./ProductForm";

type ProductCreateFormProps = {
    onCreated: () => void | Promise<void>;
    onCancel: () => void;
}

export function ProductCreateForm({
    onCreated,
    onCancel,
}: ProductCreateFormProps){
    async function handleSubmit(data: CreateProductRequest){
        const token = getAuthToken();

        if (!token){
            throw new Error("You sre not authorized");
        }

        await createProduct(token, data);

        await onCreated();
    }

    return(
        <ProductForm
            submitLabel="Создать"
            loadingLabel="Сохраняем..."
            onSubmit={handleSubmit}
            onCancel={onCancel}/>
    );
}