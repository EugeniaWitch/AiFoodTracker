"use client";

import { useState } from "react";
import type { ProductResponse } from "@/shared/types/product";
import { getProductNutritionSummary } from "../lib/productFormatting";
import { ProductDetails } from "./ProductDetails";
import styles from "./ProductList.module.css";

type ProductListItemProps = {
    product: ProductResponse;
}

export function ProductListItem({product}: ProductListItemProps){
    const [isExpanded, setIsExpanded] = useState(false);

    return(
        <li className={styles.item}>
            <div className={styles.header}>
                <div className={styles.mainInfo}>
                    <h3 className={styles.title}>{product.name} {product.brand ? `(${product.brand})` : ``}</h3>
                    <p className={styles.summary}>{getProductNutritionSummary(product)}</p>
                </div>

                <div className={styles.actions}>
                    <button
                    type="button"
                    className={styles.button}
                    onClick={() => setIsExpanded((current) => !current)}>
                        {isExpanded ? "Скрыть" : "Подробнее"}
                    </button>
                </div>
            </div>
            
            {isExpanded && <ProductDetails product={product}/>}
        </li>
    )
}