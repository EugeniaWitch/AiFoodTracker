import type { ProductType } from "@/shared/types/product";
import { productTypes } from "../lib/productFormatting";
import styles from "./ProductFilters.module.css";

type ProductFiltersProps ={
    selectedType: ProductType;
    search: string;
    onTypeChange: (type: ProductType) => void;
    onSearchChange: (search: string) => void;
    onSearchSubmit: () => void;
};

export function ProductFilters({
    selectedType,
    search,
    onTypeChange,
    onSearchChange,
    onSearchSubmit
}: ProductFiltersProps){
    return (
        <section className={styles.filters}>
            <div className={styles.select}>
                <label>Тип</label>
                <select
                value={selectedType}
                onChange={(event) => onTypeChange(event.target.value as ProductType)}>
                    {productTypes.map((type) => (<option key={type} value={type}>
                        {type=== "Food" ? "Еда" : "Напитки"}
                    </option>))}
                </select>
            </div>
           
            <div className={styles.search}>
                <label>Поиск</label>
                <input className={styles.input}
                value={search}
                onChange={(event) => onSearchChange(event.target.value)}
                placeholder="Например: сосиски"></input>
            </div>
            
            <button type="button" onClick={onSearchSubmit} className={styles.button}>
                Найти
            </button>
        </section>
    );
}