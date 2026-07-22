export type ProductType = "Food"  | "Drink";

export type ProductVisibility = "Public" | "Private";

export type ProductUnit = "Gram" | "Ml" | "Portion";

export type ProductSourceType = "Custom" | "Branded";

export type ProductReviewStatus = "NotSubmitted" | "PendingReview" | "Approved" | "Rejected";

export type ProductResponse = {
    id: string;
    name: string;
    brand: string | null;

    sourceType:ProductSourceType;
    reviewStatus: ProductReviewStatus;
    type: ProductType;
    visibility: ProductVisibility;

    categoryId: string;
    categoryName: string;

    ownerId: string | null;

    calories: number;
    protein: number;
    fat: number;
    carbs: number;

    sugar: number | null;
    fiber: number | null;
    ironMg: number | null;

    defaultUnit: ProductUnit;

    nutritionAmount: number;
    nutritionUnit: ProductUnit;

    servingSize: number | null;
    servingSizeUnit: ProductUnit | null;
    servingDescription: string | null;

    createdAt: string;
    updatedAt: string;
}

export type CreateProductRequest ={
    name: string;
    type: ProductType;
    brand?: string | null;
    categoryId: string;
    sourceType:ProductSourceType;

    calories: number;
    protein: number;
    fat: number;
    carbs: number;

    sugar?: number | null;
    fiber?: number | null;
    ironMg?: number | null;

    defaultUnit: ProductUnit;

    nutritionUnit: ProductUnit;
    nutritionAmount: number;

    servingSize?: number | null;
    servingDescription?: string | null;
}

export type UpdateProductRequest = CreateProductRequest;