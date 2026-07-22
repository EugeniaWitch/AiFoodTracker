"use client";

import { type FormEvent, useEffect, useState } from "react";
import { getProductCategories } from "@/shared/api/productCategories";
import type{
    CreateProductRequest,
    ProductResponse,
    ProductSourceType,
    ProductType,
    ProductUnit,
} from "@/shared/types/product";
import type { ProductCategoryResponse } from "@/shared/types/productCategory";
import {
    getBaseUnit,
    getBaseUnitLabel,
    getServingSizeLabel,
    productTypes,
} from "../lib/productFormatting";
import styles from "./ProductForm.module.css";

type NutritionMode = "Base" | "Portion";

type ProductFormProps = {
    initialProduct?: ProductResponse;
    submitLabel: string;
    loadingLabel: string;
    onSubmit: (data: CreateProductRequest) => void | Promise<void>;
    onCancel: () => void;
}

export function ProductForm({
    initialProduct,
    submitLabel,
    loadingLabel,
    onSubmit,
    onCancel
}: ProductFormProps){
    const [categories, setCategories] = useState<ProductCategoryResponse[]>([]);
    const [sourceType, setSourceType] = useState<ProductSourceType>(initialProduct?.sourceType ?? "Branded");
    const [selectedType, setSelectedType] = useState<ProductType>(initialProduct?.type ?? "Food");
    const [name, setName] = useState(initialProduct?.name ?? "");
    const [brand, setBrand] = useState(initialProduct?.brand ?? "");
    const [categoryId, setCategoryId] = useState(initialProduct?.categoryId ?? "");
    const [nutritionMode, setNutritionMode] = useState(initialProduct?.nutritionUnit === "Portion" ? "Portion" : "Base");
    const [servingSize, setServingSize] = useState(initialProduct?.servingSize !== null &&
        initialProduct?.servingSize!== undefined ? String(initialProduct.servingSize) : ""
    );
    const [servingDescription, setServingDescription] = useState(initialProduct?.servingDescription ?? "");
    
    const [calories, setCalories] = useState(initialProduct ? String(initialProduct.calories) : "");
    const [protein, setProtein] = useState(initialProduct ? String(initialProduct.protein) : "");
    const [fat, setFat] = useState(initialProduct ? String(initialProduct.fat) : "");
    const [carbs, setCarbs] = useState(initialProduct ? String(initialProduct.carbs) : "");

    const [sugar, setSugar] = useState(initialProduct?.sugar !== null &&
        initialProduct?.sugar !== undefined ? String(initialProduct.sugar) : ""
    );
    const [fiber, setFiber] = useState(initialProduct?.fiber !== null &&
        initialProduct?.fiber !== undefined ? String(initialProduct.fiber) : ""
    );
    const [ironMg, setIronMg] = useState(initialProduct?.ironMg !== null &&
        initialProduct?.ironMg !== undefined ? String(initialProduct.ironMg) : ""
    );

    const [error, setError] = useState("");
    const [isSaving, setIsSaving] = useState(false);

    const baseUnit = getBaseUnit(selectedType);
    const isPortionMode = nutritionMode === "Portion";

    useEffect(() =>{
        async function loadCategories(){
            setError("");

            try{
                const data = await getProductCategories(selectedType);

                setCategories(data);
                setCategoryId((currentCategoryId) =>{
                    const currentCategoryExists = data.some((category) => category.id === currentCategoryId);
                    if (currentCategoryExists){
                        return  currentCategoryId;
                    }

                    return data[0]?.id ?? "";
                });
            } catch (error){
                if (error instanceof Error){
                    setError(error.message);
                } else{
                    setError("Failed to load categories");
                }
            }
        }
        loadCategories();
    }, [selectedType]);

    function parseOptionalNumber(value: string): number | null{
        if (value.trim() ===""){
            return null;
        }

        return Number(value);
    }

    async function handleSubmit(event:FormEvent<HTMLFormElement>){
        event.preventDefault();

        if (!categoryId){
            setError("Category is required");
            return;
        }

        setError("");
        setIsSaving(true);

        const nutritionUnit: ProductUnit = isPortionMode ? "Portion": baseUnit;
        const nutritionAmount = isPortionMode ? 1 : 100;

        try {
            await onSubmit({
                name,
                sourceType,
                brand: sourceType === "Branded" ? brand.trim() : null,
                type: selectedType,
                categoryId,
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
                servingDescription: isPortionMode ? servingDescription.trim() || null : null,
            });
        } catch (error){
            if (error instanceof Error){
                setError(error.message);
            } else{
                setError("Failed to save product");
            }
        } finally{
            setIsSaving(false);
        }
    }

    return(
        <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.name}>
                <label className={styles.title}>Название</label>
                    <input className={styles.inputLong}
                    value={name}
                    required
                    minLength={2}
                    maxLength={100}
                    onChange={(event) => setName(event.target.value)}/>
                
            </div>

            <div className={styles.brand}>
                <fieldset>
                    <legend className={styles.title}>Тип записи</legend>

                    <div className={styles.select}>
                        <div className={styles.variant}>
                            <input
                            type="radio"
                            name="sourceType"
                            value="Branded"
                            checked={sourceType === "Branded"}
                            onChange={() => setSourceType("Branded")}/>
                            <label>Брендовый продукт</label>
                        </div>
                        
                        <div className={styles.variant}>
                            <input
                            type="radio"
                            name="sourceType"
                            value="Custom"
                            checked={sourceType === "Custom"}
                            onChange={() => {setSourceType("Custom");
                                setBrand("");
                            }}/>
                            <label>Свой продукт</label>
                        </div>
                        
                    </div>
                
                </fieldset>

                {sourceType === "Branded" &&(
                    <div className={styles.lineIn}>
                        <label>Производитель (бренд)</label>
                            <input className={styles.inputLong}
                            value={brand}
                            required
                            minLength={2}
                            maxLength={100}
                            placeholder="Например: Макфа, Milka, Danone"
                            onChange={(event) => setBrand(event.target.value)}/>
                    </div>
                )}
            </div>
            
           

            <div className={styles.line}>
                <label className={styles.title}>Тип</label>
                    <select
                    value={selectedType}
                    onChange={(event) => setSelectedType(event.target.value as ProductType)}>
                        {productTypes.map((type) => (<option key={type} value={type}>
                            {type === "Food" ? "Еда" : "Напитки"}
                        </option>))}
                    </select>
            </div>

            <div className={styles.line}>
                <label className={styles.title}>Категория</label>
                    <select
                    value={categoryId}
                    onChange={(event) => setCategoryId(event.target.value)}>
                        {categories.map((category) => (<option key={category.id} value={category.id}>
                            {category.name}
                        </option>))}
                    </select>
            </div>

            <div className={styles.isPortion}>
                <fieldset>
                    <legend className={styles.title}>КБЖУ указаны на</legend>

                    <div className={styles.select}>
                        <div className={styles.variant}>
                            <input
                            type="radio"
                            name="nutritionMode"
                            value="Base"
                            checked={nutritionMode === "Base"}
                            onChange={() => setNutritionMode("Base")}/>
                            <label>{getBaseUnitLabel(selectedType)}</label>
                        </div>
                        
                        <div className={styles.variant}>
                            <input
                            type="radio"
                            name="nutritionMode"
                            value="Portion"
                            checked={nutritionMode === "Portion"}
                            onChange={() => setNutritionMode("Portion")}/>
                            <label>1 порцию</label>
                        </div>
                    </div>
                </fieldset>

                {isPortionMode &&(
                    <fieldset className={styles.portion}>
                        <legend className={styles.title}>Порция</legend>

                        <div className={styles.lineIn}>
                            <label>1 порция =</label>
                                <input className={styles.input}
                                type="number"
                                value={servingSize}
                                required
                                min={0.0001}
                                step="any"
                                onChange={(event) => setServingSize(event.target.value)}/>
                            <label>{getServingSizeLabel(selectedType)}</label>
                        </div>

                        <div className={styles.lineIn}>
                            <label>Описание порции</label>
                                <input className={styles.inputLong}
                                value={servingDescription}
                                maxLength={100}
                                placeholder="Например: 1 конфета, 3 дольки"
                                onChange={(event) => setServingDescription(event.target.value)}/>
                        </div>
                    </fieldset>
                )}    
            </div>
            

            <fieldset className={styles.cpfc}>
                <legend className={styles.title}>
                    КБЖУ на {isPortionMode ? "1 порцию" : getBaseUnitLabel(selectedType)}
                </legend>

                <div className={styles.nutrient}>
                    <label>Калории</label>
                        <input className={styles.input}
                        type="number"
                        value={calories}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setCalories(event.target.value)}/>
                </div>

                <div className={styles.nutrient}>
                    <label>Белки</label>
                        <input className={styles.input}
                        type="number"
                        value={protein}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setProtein(event.target.value)}/>
                </div>

                <div className={styles.nutrient}>
                    <label>Жиры</label>
                        <input className={styles.input}
                        type="number"
                        value={fat}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setFat(event.target.value)}/>
                </div>
                
                <div className={styles.nutrient}>
                    <label>Углеводы</label>
                        <input className={styles.input}
                        type="number"
                        value={carbs}
                        required
                        min={0}
                        step="any"
                        onChange={(event) => setCarbs(event.target.value)}/>
                </div>

                <div className={styles.nutrient}>
                    <label>Сахар</label>
                        <input className={styles.input}
                        type="number"
                        value={sugar}
                        min={0}
                        step="any"
                        onChange={(event) => setSugar(event.target.value)}/>
                </div>

                <div className={styles.nutrient}>
                    <label>Клетчатка</label>
                        <input className={styles.input}
                        type="number"
                        value={fiber}
                        min={0}
                        step="any"
                        onChange={(event) => setFiber(event.target.value)}/>
                </div>

                <div className={styles.nutrient}>
                    <label>Железо, мг</label>
                        <input className={styles.input}
                        type="number"
                        value={ironMg}
                        min={0}
                        step="any"
                        onChange={(event) => setIronMg(event.target.value)}/>
                </div>
            </fieldset>

            {error && <p>{error}</p>}

            <div className={styles.actions}>
                <button type="submit" disabled={isSaving} className={styles.button}>
                    {isSaving ? loadingLabel : submitLabel}
                </button>

                <button type="button" onClick={onCancel} className={styles.button}>
                    Отмена
                </button>
            </div>
            
        </form>
    );
} 