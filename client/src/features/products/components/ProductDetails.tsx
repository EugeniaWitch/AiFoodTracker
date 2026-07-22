import type { ProductResponse } from "@/shared/types/product";
import { formatNumber, getUnitLabel } from "../lib/productFormatting";
import styles from "./ProductList.module.css";

type ProductDetailsProps = {
    product: ProductResponse;
    onEdit: (product: ProductResponse) => void;
}

export function ProductDetails({product, onEdit}:ProductDetailsProps){
    return(
        <div className={styles.details}>
            <div className={styles.nutritionGrid}>
                <NutritionCell label="Калории" value={`${formatNumber(product.calories)} ккал`}></NutritionCell>
                <NutritionCell label="Белки" value={`${formatNumber(product.protein)} г`}></NutritionCell>
                <NutritionCell label="Жиры" value={`${formatNumber(product.fat)} г`}></NutritionCell>
                <NutritionCell label="Углеводы" value={`${formatNumber(product.carbs)} г`}></NutritionCell>
                {product.sugar !== null && 
                    <NutritionCell label="Сахар" value={`${formatNumber(product.sugar)} г`}></NutritionCell>}
                {product.fiber !== null &&
                    <NutritionCell label="Клетчатка" value={`${formatNumber(product.fiber)} г`}></NutritionCell>}
                {product.ironMg !== null &&
                    <NutritionCell label="Железо" value={`${formatNumber(product.ironMg)} мг`}></NutritionCell>}            
            </div>

            <div className={styles.detailsActions}>
                <button
                    type="button"
                    className={styles.button}
                    onClick={() => onEdit(product)}>
                        Изменить КБЖУ
                    </button>
            </div>
        </div>
    );
}

type NutritionCellProps = {
    label: string;
    value: string;
}

function NutritionCell({label,value}:NutritionCellProps){
    return (
        <div className={styles.nutritionCell}>
            <span className={styles.nutritionLabel}>{label}</span>
            <span className={styles.nutritionValue}>{value}</span>
        </div>
    );
}