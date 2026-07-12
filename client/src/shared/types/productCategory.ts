export type ProductCategoryType = "Food" | "Drink" | "Dish"

export type ProductCategoryResponse = {
    id: string;
    name: string;
    type: ProductCategoryType;
    icon: string | null;
}