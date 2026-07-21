"use client";

import { type FormEvent, useEffect, useState } from "react";
import { getProductCategories } from "@/shared/api/productCategories";
import { createProduct } from "@/shared/api/products";
import { getAuthToken } from "@/shared/lib/authToken";
import type{
    ProductType,
    ProductUnit,
    ProductVisibility
} from "@/shared/types/product";
import type { ProductCategoryResponse } from "@/shared/types/productCategory";
import {
    getBaseUnit,
    getBaseUnitLabel,
    getServingSizeLabel,
    productTypes,
    productVisibilities
} from "../lib/productFormatting";
import { parse } from "path";

type NutritionMode = "Base" | "Portion";

type ProductCreateFormProps = {
    onCreated: () => void | Promise<void>;
    onCancel: () => void;
}

export function ProductCreateForm({
    onCreated,
    onCancel,
}: ProductCreateFormProps){
    const [categories, setCategories] = useState<ProductCategoryResponse[]>([]);

    const [selectedType, setSelectedType] = useState<ProductType>("Food");
    const [name, setName] = useState("");
    const [brand, setBrand] = useState("");
    const [categoryId, setCategoryId] = useState("");
    const [visibility, setVisibility] = useState<ProductVisibility>("Private");

    const [nutritionMode, setNutritionMode] = useState<NutritionMode>("Base");

    const [servingSize, setServingSize] = useState("");
    const [servingDescription, setServingDescription] = useState("");

    const [calories, setCalories] = useState("");
    const [protein, setProtein] = useState("");
    const [fat, setFat] = useState("");
    const [carbs, setCarbs] = useState("");

    const [sugar, setSugar] = useState("");
    const [fiber, setFiber] = useState("");
    const [ironMg, setIronMg] = useState("");

    const [error, setError] = useState("");
    const [isSaving, setIsSaving] = useState(false);

    const baseUnit = getBaseUnit(selectedType);
    const isPortionMode = nutritionMode === "Portion";

    useEffect(() => {
        async function loadCategories(){
            setError("");

            try{
                const data = await getProductCategories(selectedType);
                setCategories(data);
                setCategoryId(data[0]?.id ?? "");
            } catch (error){
                if (error instanceof Error){
                    setError(error.message);
                } else{
                    setError("Faield to load categories");
                }
            }
        }

        setNutritionMode("Base");
        setServingSize("");
        setServingDescription("");

        loadCategories();
    }, [selectedType]);

    function parseOptionalNumber(value: string): number | null{
        if (value.trim() === ""){
            return null;
        }

        return Number(value);
    }

    function resetForm(){
        setName("");
        setBrand("");
        setNutritionMode("Base");
        setServingSize("");
        setServingDescription("");

        setCalories("");
        setProtein("");
        setFat("");
        setCarbs("");
        setSugar("");
        setFiber("");
        setIronMg("");
    }

    async function handleSubmit(event: FormEvent<HTMLFormElement>){
        event.preventDefault();

        const token = getAuthToken();

        if (!token){
            setError("You are not authorized");
            return;
        }

        if (!categoryId){
            setError("Category is required");
            return;
        }

        setError("");
        setIsSaving(true);

        const nutritionUnit: ProductUnit = isPortionMode ? "Portion" : baseUnit;
        const nutritionAmount = isPortionMode ? 1 : 100;
        
        try{
            await createProduct(token, {
                name,
                brand: brand.trim() || null,
                type: selectedType,
                categoryId,
                visibility,
                defaultUnit: nutritionUnit,
                nutritionAmount,
                nutritionUnit,
                calories: Number(calories),
                protein: Number(protein),
                fat: Number(fat),
                carbs: Number(carbs),
                sugar: parseOptionalNumber(sugar),
                fiber: parseOptionalNumber(fiber),
                ironMg: parseOptionalNumber(ironMg),
                servingSize: isPortionMode ? Number(servingSize) : null,
                servingDescription: isPortionMode ? servingDescription.trim() || null : null
            });

            resetForm();

            await onCreated();
        } catch (error){
            if (error instanceof Error){
                setError(error.message);
            } else{
                setError("Failed to create product");
            }
        } finally{
            setIsSaving(false);
        }
    }

    return(
        <form onSubmit={handleSubmit}>
            <div>
                <label>
                    Название:
                    <input
                    value={name}
                    required
                    minLength={2}
                    maxLength={100}
                    onChange={(event) => setName(event.target.value)}/>
                </label>
            </div>

            <div>
                <label>
                    Производитель (бренд):
                    <input
                    value={brand}
                    minLength={2}
                    maxLength={100}
                    placeholder="Например: Макфа, Milka, Danone"
                    onChange={(event) => setBrand(event.target.value)}/>
                </label>
            </div>

            <div>
                <label>
                    Тип:
                    <select
                    value={selectedType}
                    onChange={(event) => setSelectedType(event.target.value as ProductType)}>
                        {productTypes.map((type) => (<option key={type} value={type}>
                            {type === "Food" ? "Еда" : "Напитки"}
                        </option>))}
                    </select>
                </label>
            </div>

            <div>
                <label>
                    Категория:
                    <select
                    value={categoryId}
                    onChange={(event) => setCategoryId(event.target.value)}>
                        {categories.map((category) => (<option key={category.id} value={category.id}>
                            {category.name}
                        </option>))}
                    </select>
                </label>
            </div>

            <fieldset>
                <legend>КБЖУ указаны на</legend>

                <label>
                    <input
                    type="radio"
                    name="nutritionMode"
                    value="Base"
                    checked={nutritionMode === "Base"}
                    onChange={() => setNutritionMode("Base")}/>
                    {getBaseUnitLabel(selectedType)}
                </label>

                <label>
                    <input
                    type="radio"
                    name="nutritionMode"
                    value="Portion"
                    checked={nutritionMode === "Portion"}
                    onChange={() => setNutritionMode("Portion")}/>
                    1 порцию
                </label>
            </fieldset>

            {isPortionMode &&(
                <fieldset>
                    <legend>Порция</legend>

                    <div>
                        <label>
                            1 порция = 
                            <input
                            type="number"
                            value={servingSize}
                            required
                            min={0.0001}
                            step="any"
                            onChange={(event) => setServingSize(event.target.value)}/>
                            {getServingSizeLabel(selectedType)}
                        </label>
                    </div>

                    <div>
                        <label>
                            Описание порции:
                            <input
                            value={servingDescription}
                            maxLength={100}
                            placeholder="Например: 1 конфета, 3 дольки, 1 банка"
                            onChange={(event) => setServingDescription(event.target.value)}/>
                        </label>
                    </div>
                </fieldset>
            )}

            <fieldset>
                <legend>
                    КБЖУ на {isPortionMode ? "1 порцию" : getBaseUnitLabel(selectedType)}
                </legend>

                <div>
                    <label>
                        Калории:
                        <input
                        type="number"
                        value={calories}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setCalories(event.target.value)}/>
                    </label>
                </div>

                <div>
                    <label>
                        Белки:
                        <input
                        type="number"
                        value={protein}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setProtein(event.target.value)}/>
                    </label>
                </div>

                <div>
                    <label>
                        Жиры:
                        <input
                        type="number"
                        value={fat}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setFat(event.target.value)}/>
                    </label>
                </div>
                
                <div>
                    <label>
                        Углеводы:
                        <input
                        type="number"
                        value={carbs}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setCarbs(event.target.value)}/>
                    </label>
                </div>

                <div>
                    <label>
                        Сахар:
                        <input
                        type="number"
                        value={sugar}
                        min={0}
                        step="any"
                        onChange={(event) => setSugar(event.target.value)}/>
                    </label>
                </div>

                <div>
                    <label>
                        Клетчатка:
                        <input
                        type="number"
                        value={fiber}
                        min={0}
                        step="any"
                        onChange={(event) => setFiber(event.target.value)}/>
                    </label>
                </div>

                <div>
                    <label>
                        Железо, мг:
                        <input
                        type="number"
                        value={ironMg}
                        min={0}
                        step="any"
                        onChange={(event) => setIronMg(event.target.value)}/>
                    </label>
                </div>
            </fieldset>

            {error && <p>{error}</p>}

            <button type="submit" disabled={isSaving}>
                {isSaving ? "Сохраняем..." : "Создать"}
            </button>

            <button type="button" onClick={onCancel}>
                Отмена
            </button>
        </form>
    );
}