"use client";

import { updateProduct } from "@/shared/api/products";
import { getAuthToken } from "@/shared/lib/authToken";
import type {
    ProductResponse,
    UpdateProductRequest,
} from "@/shared/types/product";
import { ProductForm } from "./ProductForm";

type ProductEditFormProps = {
    product: ProductResponse;
    onUpdated: () => void | Promise<void>;
    onCancel: () => void;
};

export function ProductEditForm({
    product,
    onUpdated,
    onCancel,
}: ProductEditFormProps){
    async function handleSubmit(data:UpdateProductRequest){
        const token = getAuthToken();

        if (!token){
            throw new Error("Yoy are not authorized");
        }

        await updateProduct(token,product.id,data);

        await onUpdated();
    }

    return(
        <ProductForm
            initialProduct={product}
            submitLabel="Сохранить изменения"
            loadingLabel="Сохраняем..."
            onSubmit={handleSubmit}
            onCancel={onCancel}/>
    )
}