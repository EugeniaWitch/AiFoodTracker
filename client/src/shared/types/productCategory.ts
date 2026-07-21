export type ProductCategoryType = "Food" | "Drink"

export type ProductCategoryResponse = {
    id: string;
    name: string;
    type: ProductCategoryType;
    icon: string | null;
}