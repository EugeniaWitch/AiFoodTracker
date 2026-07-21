import type{
    ProductResponse,
    ProductType,
    ProductUnit,
    ProductVisibility
} from "@/shared/types/product";

export const productTypes: ProductType[] = ["Food", "Drink"];

export const productVisibilities: ProductVisibility[] = ["Private", "Public"];

export function getBaseUnit(type: ProductType): ProductUnit{
    return type === "Drink" ? "Ml" : "Gram";
}

export function getBaseUnitLabel(type: ProductType): string{
    return type === "Drink" ? "100 мл" : "100 г";
}

export function getServingSizeLabel(type: ProductType):string{
    return type === "Drink" ? "мл" : "г";
}

export function getUnitLabel(unit: ProductUnit):string{
    switch(unit){
        case "Gram":
            return "г";
        case "Ml":
            return "мл";
        case "Portion":
            return "порция";
    }
}

export function formatNumber(value: number): string{
    return Number.isInteger(value) ? String(value) : String(value);
}

export function getProductNutritionSummary(product: ProductResponse):string{
    if (product.nutritionUnit === "Portion"){
        const portionName = product.servingDescription?.trim() || "1 порция";

        const portionSize = product.servingSize && product.servingSizeUnit ? 
            `(${formatNumber(product.servingSize)} ${getUnitLabel(product.servingSizeUnit)})` : "";

        return `${portionName} ${portionSize} · ${formatNumber(product.calories)} ккал`;
    }

    return `${formatNumber(product.nutritionAmount)} ${getUnitLabel(product.nutritionUnit)} · ${formatNumber(product.calories)} ккал`;
}