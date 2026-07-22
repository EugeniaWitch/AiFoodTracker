"use client";

import { type FormEvent, useEffect, useState } from "react";
import { useCurrentUser } from "@/features/auth/hooks/useCurrentUser";
import { getProducts } from "@/shared/api/products";
import { getAuthToken } from "@/shared/lib/authToken";
import type {
  ProductResponse,
  ProductType,
} from "@/shared/types/product";
import { ProductFilters } from "@/features/products/components/ProductFilters";
import { ProductList } from "@/features/products/components/ProductList";
import { ProductModal } from "@/features/products/components/ProductModal";
import { ProductCreateForm } from "@/features/products/components/ProductCreateForm";
import { ProductEditForm } from "@/features/products/components/ProductEditForm";
import styles from "./page.module.css";

export default function ProductsPage() {
  const { isLoading: isUserLoading } = useCurrentUser();

  const [products, setProducts] = useState<ProductResponse[]>([]);

  const [selectedType, setSelectedType] = useState<ProductType>("Food");
  const [search, setSearch] = useState("");
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

  const [error, setError] = useState("");
  const [isLoadingProducts, setIsLoadingProducts] = useState(false);

  const [editingProduct, setEditingProduct] = useState<ProductResponse | null>(null);

  async function loadProducts() {
    const token = getAuthToken();

    if (!token) {
      return;
    }

    setIsLoadingProducts(true);
    setError("");

    try {
      const data = await getProducts(token, {
        type: selectedType,
        search,
      });

      setProducts(data);
    } catch (error) {
      if (error instanceof Error) {
        setError(error.message);
      } else {
        setError("Failed to load products");
      }
    } finally {
      setIsLoadingProducts(false);
    }
  }

  useEffect(() => {
    if (!isUserLoading) {
      loadProducts();
    }
  }, [isUserLoading, selectedType]);

  if (isUserLoading) {
    return <main>Загрузка...</main>;
  }

  return (
    <main className={styles.page}>
      <div className={styles.header}>
        <ProductFilters
        selectedType={selectedType}
        search={search}
        onTypeChange={setSelectedType}
        onSearchChange={setSearch}
        onSearchSubmit={loadProducts}></ProductFilters>

        <button type="button" onClick={() => setIsCreateModalOpen(true)} className={styles.button}>
          Добавить новый продукт
        </button>
      </div>
      

      <ProductModal
        title="Добавить новый продукт"
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}>
          <ProductCreateForm
          onCreated={async () => {
            await loadProducts();
            setIsCreateModalOpen(false);
          }}
          onCancel={() => setIsCreateModalOpen(false)}/>
      </ProductModal>

      <ProductModal
        title="Изменить КБЖУ"
        isOpen={editingProduct !==null}
        onClose={() => setEditingProduct(null)}>
          {editingProduct &&
            <ProductEditForm
              product={editingProduct}
              onUpdated={async () => {
                await loadProducts();
                setEditingProduct(null);
              }}
              onCancel={() => setEditingProduct(null)}/>}
      </ProductModal>

      <ProductList 
        products={products}
        isLoading={isLoadingProducts}
        onEdit={setEditingProduct}/>
    </main>
  );
}