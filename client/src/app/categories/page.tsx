"use client";

import { use, useEffect, useState } from "react";
import { getProductCategories } from "@/shared/api/productCategories";
import type {
    ProductCategoryResponse,
    ProductCategoryType
} from "@/shared/types/productCategory";
import { sendTaskMessage } from "next/dist/build/swc/generated-native";

export default function CategoriesPage(){
    const [categories, setCategories] = useState<ProductCategoryResponse[]>([]);
    const [selectedType, setSelectedType] = useState<ProductCategoryType | undefined>();
    const [error, setError] = useState("");
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        async function loadCategories(){
            setIsLoading(true);
            setError("");

            try{
                const data = await getProductCategories(selectedType);
                setCategories(data);
            } catch (error){
                if (error instanceof Error){
                    setError(error.message);
                }else{
                    setError("Failed to load categories");
                }
            } finally{
                setIsLoading(false);
            }
        }
        loadCategories();
    }, [selectedType]);

    return(
        <main>
            <h1>Категория продуктов</h1>

            <div>
                <button type="button" onClick={() => setSelectedType(undefined)}>Все</button>
                <button type="button" onClick={() => setSelectedType("Food")}>Еда</button>
                <button type="button" onClick={() => setSelectedType("Drink")}>Напитки</button>
            </div>

            {isLoading && <p>Загрузка</p>}

            {error && <p>{error}</p>}

            {!isLoading && !error && (
                <ul>
                    {categories.map((category) => (
                        <li key={category.id}>
                            {category.name} - {category.type}
                        </li>
                    ))}
                </ul>
            )}
        </main>
    )
}
