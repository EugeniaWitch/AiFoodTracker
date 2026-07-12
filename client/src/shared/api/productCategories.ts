import type {
    ProductCategoryResponse,
    ProductCategoryType
} from "@/shared/types/productCategory";

const API_URL = (process.env.NEXT_PUBLIC_API_URL || "http://localhost:4000").replace(/\/$/,"");

export async function getProductCategories(type?: ProductCategoryType) : Promise<ProductCategoryResponse[]>{
    const url = type ? `${API_URL}/api/product-categories?type=${type}` : `${API_URL}/api/product-categories`;

    const response = await fetch(url);

    if (!response.ok){
        throw new Error(`Failed to load product categories. Status: ${response.status}`);
    }

    return response.json();
}