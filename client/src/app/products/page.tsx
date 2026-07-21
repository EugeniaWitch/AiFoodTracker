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

export default function ProductsPage() {
  const { isLoading: isUserLoading } = useCurrentUser();

  const [products, setProducts] = useState<ProductResponse[]>([]);

  const [selectedType, setSelectedType] = useState<ProductType>("Food");
  const [search, setSearch] = useState("");
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);

  const [error, setError] = useState("");
  const [isLoadingProducts, setIsLoadingProducts] = useState(false);

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
    <main>
      <h1>Продукты</h1>

      <ProductFilters
      selectedType={selectedType}
      search={search}
      onTypeChange={setSelectedType}
      onSearchChange={setSearch}
      onSearchSubmit={loadProducts}></ProductFilters>

      <button type="button" onClick={() => setIsCreateModalOpen(true)}>
        Добавить новый продукт
      </button>

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

      <section>
        <h2>Список продуктов</h2>

        <ProductList products={products}
        isLoading={isLoadingProducts}></ProductList>
      </section>
    </main>
  );
}