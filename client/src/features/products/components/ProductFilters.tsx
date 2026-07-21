import type { ProductType } from "@/shared/types/product";
import { productTypes } from "../lib/productFormatting";

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
        <section>
            <h2>Фильтр</h2>

            <label>
                Тип:
                <select
                value={selectedType}
                onChange={(event) => onTypeChange(event.target.value as ProductType)}>
                    {productTypes.map((type) => (<option key={type} value={type}>
                        {type=== "Food" ? "Еда" : "Напитки"}
                    </option>))}
                </select>
            </label>

            <label>
                Поиск:
                <input
                value={search}
                onChange={(event) => onSearchChange(event.target.value)}
                placeholder="Например: сосиски"></input>
            </label>

            <button type="button" onClick={onSearchSubmit}>
                Найти
            </button>
        </section>
    );
}