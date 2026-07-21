import type { ProductResponse } from "@/shared/types/product";
import { ProductListItem } from "./ProductListItem";
import styles from "./ProductList.module.css";

type ProductListProps ={
    products: ProductResponse[];
    isLoading: boolean;
}

export function ProductList({products,isLoading}: ProductListProps){
    if (isLoading){
        return <p>Загрузка продуктов</p>
    }

    if (products.length===0){
        return <p>Продуктов пока что нет</p>
    }

    return (
        <ul className={styles.list}>
            {products.map((product) => 
                (<ProductListItem key={product.id} product={product}></ProductListItem>))}
        </ul>
    )
}