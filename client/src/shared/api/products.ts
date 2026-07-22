import type {
    CreateProductRequest,
    ProductResponse,
    ProductType,
    UpdateProductRequest
} from "@/shared/types/product";

const API_URL = (process.env.NEXT_PUBLIC_API_URL || "http://localhost:4000").replace(/\/$/,"");

function getErrorMessage(error: unknown, fallback: string) : string {
    if (typeof error == "object" && 
        error !==null && 
        "message" in error && 
        typeof error.message =="string"){
        return error.message;
    }

    if (typeof error == "object" && 
        error !==null && 
        "errors" in error && 
        typeof error.errors =="object" &&
        error.errors!==null){
        const firstError = Object.values(error.errors).flat().at(0);
        if (typeof firstError === "string"){
            return firstError;
        }
    }

    return fallback;
}

export async function getProducts(token:string,params?:{
    type?: ProductType,
    search?: string,
}) : Promise<ProductResponse[]> {
    const searchParams = new URLSearchParams();

    if (params?.type){
        searchParams.set("type",params.type);
    }

    if (params?.search){
        searchParams.set("search",params.search);
    }

    const queryString = searchParams.toString();

    const url = queryString ? `${API_URL}/api/products?${queryString}`:`${API_URL}/api/products`;
    
    const response = await fetch(url, {headers: {
        Authorization: `Bearer ${token}`,
    },
    });

    if (!response.ok){
        throw new Error(`Failed to load products. Status: ${response.status}`);
    }
    return response.json();
}

export async function createProduct(token:string, data:CreateProductRequest):Promise<ProductResponse>{
    const response = await fetch(`${API_URL}/api/products`,{
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });

    if (!response.ok){
        const contentType = response.headers.get("content-type");

        if (contentType?.includes("json")){
            const error = await response.json();
            throw new Error(getErrorMessage(error, `Failed to create product. Status: ${response.status}`));
        }

        throw new Error(`Failed to create product. Status: ${response.status}`);
    }

    return response.json();
}

export async function updateProduct(
    token: string,
    productId: string,
    data: UpdateProductRequest,
): Promise<ProductResponse>{
    const response = await fetch(`${API_URL}/api/products/${productId}`,{
        method:"PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });

    if (!response.ok){
        const contentType = response.headers.get("content-type");

        if (contentType?.includes("json")){
            const error = await response.json();

            throw new Error(getErrorMessage(error,`Failed to update product. Status: ${response.status}`));
        }

        throw new Error(`Failed to load update product. Status: ${response.status}`);
    }

    return response.json();
}